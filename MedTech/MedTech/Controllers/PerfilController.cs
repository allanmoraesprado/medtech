using Application.Interfaces;
using Application.Models;
using Infraestructure.StatusSistema;
using Infraestructure.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IUserServiceApplication _userServiceApplication;
        private readonly ILogServiceApplication _logServiceApplication;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PerfilController(IUserServiceApplication userServiceApplication,            
            ILogServiceApplication logServiceApplication,
            IHttpContextAccessor httpContext)
        {
            _userServiceApplication = userServiceApplication;
            _logServiceApplication = logServiceApplication;
            _httpContextAccessor = httpContext;
        }

        public IActionResult Index(UsuarioViewModel model, IFormFile foto, int? id)
        {
            try
            {
                int autenticacao = (int)_httpContextAccessor.HttpContext.Session.GetInt32(Session.Acesso);

                if (autenticacao == 1)
                {
                    var username = _httpContextAccessor.HttpContext.Session.GetString(Session.Usuario);
                    var senha = _httpContextAccessor.HttpContext.Session.GetString(Session.Senha);

                    var usuarioExiste = _userServiceApplication.Validar(username);

                    if (usuarioExiste)
                    {
                        var usuario = _userServiceApplication.Buscar(username, senha);
                        model.Usuario = username;

                        if (usuario != null)
                        {
                            if (id == null)
                            {
                                if (foto != null)
                                {
                                    var dir = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\perfil\\" + usuario.Usuario + "\\";
                                    var nomeImagem = Guid.NewGuid().ToString() + "_" + foto.FileName;

                                    if (!Directory.Exists(dir))
                                    {
                                        Directory.CreateDirectory(dir);
                                    }
                                    using (var stream = System.IO.File.Create(dir + nomeImagem))
                                    {
                                        foto.CopyToAsync(stream);
                                    }
                                }
                                else
                                {
                                    if (Helpers.Tools.ValidUserModel(model))
                                    {
                                        model.Senha = senha;
                                        _userServiceApplication.Atualizar(model);
                                        ViewData["Retorno"] = RetornoCodigo.DADOS_ATUALIZADOS.ToDescription();
                                    }
                                }
                            }
                            else
                            {
                                ViewBag.Details = true;
                                model = usuario;
                            }
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

            return View(model);
        }
    }
}
