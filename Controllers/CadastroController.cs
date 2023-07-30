using Application.Interfaces;
using Application.Models;
using Infraestructure.StatusSistema;
using Infraestructure.Tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class CadastroController : Controller
    {
        private readonly IUserServiceApplication _userServiceApplication;
        private readonly ILogServiceApplication _logServiceApplication;
        public CadastroController(IUserServiceApplication userServiceApplication,
            ILogServiceApplication logServiceApplication)
        {
            _userServiceApplication = userServiceApplication;
            _logServiceApplication = logServiceApplication;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExiste = _userServiceApplication.Validar(model.Usuario);

                    if (!usuarioExiste)
                    {
                        var existeCpf = _userServiceApplication.ValidarCpf(model.Cpf);

                        if (!existeCpf)
                        {
                            model.RoleId = (int)TipoRole.PACIENTE;
                            _userServiceApplication.Cadastrar(model);
                            ViewData["Retorno"] = RetornoCodigo.SUCESSO_CADASTRO.ToDescription();
                        }
                        else
                        {
                            ViewData["Retorno"] = RetornoCodigo.CPF_EXISTENTE.ToDescription();
                        }
                    }
                    else
                    {
                        ViewData["Retorno"] = RetornoCodigo.USUARIO_JA_EXISTE.ToDescription();
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
                    ViewData["Retorno"] = RetornoCodigo.ERRO_CADASTRO.ToDescription();
                }
            }
            return View(model);
        }
    }
}
