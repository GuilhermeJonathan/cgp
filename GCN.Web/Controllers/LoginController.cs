using Campeonato.Aplicacao.Login;
using Campeonato.Aplicacao.Login.Modelos;
using Campeonato.Web.CustomExtensions;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicoDeLogin _servicoDeLogin;

        public LoginController(IServicoDeLogin servicoDeLogin)
        {
            _servicoDeLogin = servicoDeLogin;
        }

        public ActionResult Index()
        {
            if (User.Autenticado())
            {
                var usuario = User.Logado();
                ViewBag.Usuario = usuario.Nome;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Entrar(string login, string senha)
        {
            if(!User.Autenticado())
                this._servicoDeLogin.Entrar(new ModeloDeLogin(login, senha, Request.UserHostAddress));

    
            return RedirectToAction(nameof(Index), "Home");
        }


        [HttpGet]
        public ActionResult Sair()
        {
            this._servicoDeLogin.Sair();
            return RedirectToAction(nameof(Index), "Login");
        }
    }
}