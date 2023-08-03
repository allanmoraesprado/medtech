using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infraestructure.StatusSistema;
using System.Reflection;
using System.Diagnostics;
using System;
using System.Collections;
using Json.Net;
using System.Collections.Generic;
using System.Linq;
using Infraestructure.Tools;

namespace Application.Controllers
{
    public class FichaController : Controller
    {
        private readonly IUserServiceApplication _userServiceApplication;
        private readonly IMedServiceApplication _medServiceApplication;
        private readonly ILogServiceApplication _logServiceApplication;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FichaController(IUserServiceApplication userServiceApplication,
            IMedServiceApplication medServiceApplication,
            ILogServiceApplication logServiceApplication,
            IHttpContextAccessor httpContext)
        {
            _userServiceApplication = userServiceApplication;
            _medServiceApplication = medServiceApplication;
            _logServiceApplication = logServiceApplication;
            _httpContextAccessor = httpContext;
        }
        [HttpGet]
        public IActionResult Index(int? id)
        {
            FichaViewModel model = new FichaViewModel();

            try
            {
                if (id != null)
                {
                    int autenticacao = (int)_httpContextAccessor.HttpContext.Session.GetInt32(Session.Acesso);

                    if (autenticacao == 1)
                    {
                        var username = _httpContextAccessor.HttpContext.Session.GetString(Session.Usuario);
                        var senha = _httpContextAccessor.HttpContext.Session.GetString(Session.Senha);
                        var autorizacao = _httpContextAccessor.HttpContext.Session.GetInt32(Session.Autorizacao);

                        var userId = _userServiceApplication.BuscarId(username, senha);
                        IEnumerable<FichaViewModel> fichas = null;

                        switch (autorizacao)
                        {
                            case 1:
                                {
                                    fichas = _medServiceApplication.FichasPaciente(userId);
                                    break;
                                }
                            case 2:
                                {
                                    fichas = _medServiceApplication.FichasMedico(userId);
                                    break;
                                }
                        }

                        if (fichas != null)
                        {
                            ViewData["Page"] = "FichaList";
                            return View(fichas);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var documento = GetType().Name;
                var metodo = MethodBase.GetCurrentMethod().Name;
                var linha = Infraestructure.Tools.Helpers.TraceLineMessage();
                var retorno = ex.Message;
                var status = RetornoStatus.Erro;

                _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Cadastrar(FichaViewModel model, int? id)
        {
            if (ModelState.IsValid && id == 1)
            {
                try
                {
                    int autenticacao = (int)_httpContextAccessor.HttpContext.Session.GetInt32(Session.Acesso);
                    if (autenticacao == 1)
                    {
                        var autorizacao = _httpContextAccessor.HttpContext.Session.GetInt32(Session.Autorizacao);

                        if (autorizacao > 1)
                        {
                            var pacienteId = _userServiceApplication.BuscarRegistro(model.CpfPaciente);
                            if (pacienteId > 0)
                            {
                                model.PacienteId = pacienteId;
                                var username = _httpContextAccessor.HttpContext.Session.GetString(Session.Usuario);
                                var senha = _httpContextAccessor.HttpContext.Session.GetString(Session.Senha);                               
                                var medicoId = _userServiceApplication.BuscarId(username, senha);
                                var usuario = _userServiceApplication.Buscar(medicoId);
                                var paciente = _userServiceApplication.Buscar(pacienteId);

                                if (model.CpfPaciente.Replace("-", ".") == paciente.Cpf)
                                {
                                    if (model.NomePaciente == paciente.Nome)
                                    {
                                        model.UsuarioPaciente = paciente.Usuario;
                                        model.NomeMedico = usuario.Nome;
                                        model.MedicoId = medicoId;
                                        model.DataCriacao = DateTime.Now.ToString();
                                        model.Id = null;

                                        _medServiceApplication.Cadastrar(model);
                                        ViewData["Retorno"] = RetornoCodigo.SUCESSO_CADASTRO.ToDescription();
                                    }
                                    else
                                    {
                                        ViewData["Retorno"] = RetornoCodigo.DADOS_INVALIDOS.ToDescription();
                                    }
                                }
                                else
                                {
                                    ViewData["Retorno"] = RetornoCodigo.CPF_INCORRETO.ToDescription();
                                }
                            }
                            else
                            {
                                ViewData["Retorno"] = RetornoCodigo.USUARIO_INDEFINIDO.ToDescription();
                            }
                        }
                        else
                        {
                            ViewData["Retorno"] = RetornoCodigo.ACESSO_NEGADO.ToDescription();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var documento = GetType().Name;
                    var metodo = MethodBase.GetCurrentMethod().Name;
                    var linha = Infraestructure.Tools.Helpers.TraceLineMessage();
                    var retorno = ex.Message;
                    var status = RetornoStatus.Erro;
                    ViewData["Retorno"] = RetornoCodigo.ERRO_CADASTRO.ToDescription();

                    _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult GetFicha(string json)
        {
            var model = JsonNet.Deserialize<FichaViewModel>(json);
            ViewBag.Detalhes = model.Detalhes;
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(FichaViewModel model, string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                ViewBag.Json = json;
                model = JsonNet.Deserialize<FichaViewModel>(json);
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    int autenticacao = (int)_httpContextAccessor.HttpContext.Session.GetInt32(Session.Acesso);
                    if (autenticacao == 1)
                    {
                        var autorizacao = _httpContextAccessor.HttpContext.Session.GetInt32(Session.Autorizacao);

                        if (autorizacao > 1)
                        {
                            var pacienteId = _userServiceApplication.BuscarRegistro(model.CpfPaciente);
                            if (pacienteId > 0)
                            {
                                var username = _httpContextAccessor.HttpContext.Session.GetString(Session.Usuario);
                                var senha = _httpContextAccessor.HttpContext.Session.GetString(Session.Senha);

                                var medicoId = _userServiceApplication.BuscarId(username, senha);
                                var ficha = _medServiceApplication.FichasMedico(medicoId).Where(p => p.Id == model.Id).FirstOrDefault();

                                if (ficha != null)
                                {
                                    ficha.DataAlteracao = DateTime.Now.ToString();
                                    _medServiceApplication.Update(model);
                                    ViewData["Retorno"] = RetornoCodigo.FICHA_ATUALIZADA.ToDescription();
                                }
                            }
                            else
                            {
                                ViewData["Retorno"] = RetornoCodigo.USUARIO_INDEFINIDO.ToDescription();
                            }
                        }
                        else
                        {
                            ViewData["Retorno"] = RetornoCodigo.ACESSO_NEGADO.ToDescription();
                        }
                    }
                }
                catch (Exception ex)
                {
                    var documento = GetType().Name;
                    var metodo = MethodBase.GetCurrentMethod().Name;
                    var linha = Infraestructure.Tools.Helpers.TraceLineMessage();
                    var retorno = ex.Message;
                    var status = RetornoStatus.Erro;
                    ViewData["Retorno"] = RetornoCodigo.UPDATE_ERRO.ToDescription();

                    _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
                }
            }

            ViewBag.RetornoFicha = 1;
            return View();
        }
        [HttpPost]
        public IActionResult Deletar(int? id)
        {
            try
            {
                int autenticacao = (int)_httpContextAccessor.HttpContext.Session.GetInt32(Session.Acesso);
                if (autenticacao == 1)
                {
                    var autorizacao = _httpContextAccessor.HttpContext.Session.GetInt32(Session.Autorizacao);

                    if (autorizacao > 1)
                    {
                        var username = _httpContextAccessor.HttpContext.Session.GetString(Session.Usuario);
                        var senha = _httpContextAccessor.HttpContext.Session.GetString(Session.Senha);

                        var medicoId = _userServiceApplication.BuscarId(username, senha);
                        var fichas = _medServiceApplication.FichasMedico(medicoId);
                        var ficha = fichas.Where(p => p.Id == id).FirstOrDefault();

                        if (ficha != null)
                        {
                            _medServiceApplication.Deletar((int)ficha.Id);
                            ViewData["Retorno"] = RetornoCodigo.FICHA_DELETADA.ToDescription();
                            ViewData["Page"] = "FichaList";                            
                        }
                        return Redirect("../../Ficha/Index/1");
                    }
                    else
                    {
                        ViewData["Retorno"] = RetornoCodigo.ACESSO_NEGADO.ToDescription();
                    }
                }
            }
            catch (Exception ex)
            {
                var documento = GetType().Name;
                var metodo = MethodBase.GetCurrentMethod().Name;
                var linha = Infraestructure.Tools.Helpers.TraceLineMessage();
                var retorno = ex.Message;
                var status = RetornoStatus.Erro;
                ViewData["Retorno"] = RetornoCodigo.ERRO_CADASTRO.ToDescription();

                _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
            }
            return View();
        }
    }
}
