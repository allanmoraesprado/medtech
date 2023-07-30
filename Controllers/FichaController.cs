using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infraestructure.StatusSistema;
using Infraestructure.Tools;
using System.Reflection;
using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            MedViewModel model = new MedViewModel();

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
                        IEnumerable<MedViewModel> fichas = null;

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
                var linha = Helpers.TraceLineMessage();
                var retorno = ex.Message;
                var status = RetornoStatus.Erro;

                _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Cadastro(MedViewModel model)
        {
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
                                model.PacienteId = pacienteId;
                                var username = _httpContextAccessor.HttpContext.Session.GetString(Session.Usuario);
                                var senha = _httpContextAccessor.HttpContext.Session.GetString(Session.Senha);
                                var medicoId = _userServiceApplication.BuscarId(username, senha);
                                model.MedicoId = medicoId;

                                _medServiceApplication.Cadastrar(model);
                                ViewData["Retorno"] = RetornoCodigo.SUCESSO_CADASTRO.ToDescription();
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
                    var linha = Helpers.TraceLineMessage();
                    var retorno = ex.Message;
                    var status = RetornoStatus.Erro;
                    ViewData["Retorno"] = RetornoCodigo.ERRO_CADASTRO.ToDescription();

                    _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Atualizacao(MedViewModel model)
        {
            if(ModelState.IsValid)
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
                catch(Exception ex)
                {
                    var documento = GetType().Name;
                    var metodo = MethodBase.GetCurrentMethod().Name;
                    var linha = Helpers.TraceLineMessage();
                    var retorno = ex.Message;
                    var status = RetornoStatus.Erro;
                    ViewData["Retorno"] = RetornoCodigo.ERRO_CADASTRO.ToDescription();

                    _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int? id)
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
                        var ficha = _medServiceApplication.FichasMedico(medicoId).Where(p => p.Id == id).FirstOrDefault();

                        if (ficha != null)
                        {
                            _medServiceApplication.Deletar((int)ficha.Id);
                            ViewData["Retorno"] = RetornoCodigo.FICHA_DELETADA.ToDescription();
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
                var linha = Helpers.TraceLineMessage();
                var retorno = ex.Message;
                var status = RetornoStatus.Erro;
                ViewData["Retorno"] = RetornoCodigo.ERRO_CADASTRO.ToDescription();

                _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
            }

            return View();
        }
    }
}
