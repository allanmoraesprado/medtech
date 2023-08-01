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
using System.IO;

namespace Application.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserServiceApplication _userServiceApplication;
        private readonly IRoleServiceApplication _roleServiceApplication;
        private readonly ILogServiceApplication _logServiceApplication;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioController(IUserServiceApplication userServiceApplication,
            IRoleServiceApplication roleServiceApplication,
            ILogServiceApplication logServiceApplication,
            IHttpContextAccessor httpContext)
        {
            _userServiceApplication = userServiceApplication;
            _roleServiceApplication = roleServiceApplication;
            _logServiceApplication = logServiceApplication;
            _httpContextAccessor = httpContext;
        }
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    _httpContextAccessor.HttpContext.Session.Clear();
                    return Redirect("../../Usuario");
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExiste = _userServiceApplication.Validar(model.Usuario);

                    if (usuarioExiste)
                    {
                        var usuario = _userServiceApplication.Buscar(model.Usuario, model.Senha);

                        if (usuario != null)
                        {
                            _httpContextAccessor.HttpContext.Session.SetString(Session.Usuario, model.Usuario);
                            _httpContextAccessor.HttpContext.Session.SetString(Session.Senha, model.Senha);
                            _httpContextAccessor.HttpContext.Session.SetInt32(Session.Acesso, 1);

                            var nivelAutorizacao = _roleServiceApplication.ObterAutorizacao((int)usuario.RoleId);
                            _httpContextAccessor.HttpContext.Session.SetInt32(Session.Autorizacao, nivelAutorizacao);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewData["Retorno"] = RetornoCodigo.SENHA_INCORRETA.ToDescription();
                        }
                    }
                    else
                    {
                        ViewData["Retorno"] = RetornoCodigo.USUARIO_INDEFINIDO.ToDescription();
                    }
                }
                catch(Exception ex)
                {
                    var documento = GetType().Name;
                    var metodo = MethodBase.GetCurrentMethod().Name;
                    var linha = Infraestructure.Tools.Helpers.TraceLineMessage();
                    var retorno = ex.Message;
                    var status = RetornoStatus.Erro;

                    _logServiceApplication.AddLogServer(documento, metodo, linha, retorno, (int)status);
                }
            }
            return View(model);
        }        
    }
}
