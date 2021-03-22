using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Login;
using Cgp.Aplicacao.Login.Modelos;
using Cgp.CustomExtensions;
using Cgp.Filter;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    [TratarErros]
    public class LoginController : Controller
    {
        private readonly IServicoDeLogin _servicoDeLogin;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public LoginController(IServicoDeLogin servicoDeLogin, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            _servicoDeLogin = servicoDeLogin;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }
       
        public ActionResult Index()
        {
            return View();
        }

        [RecuperarValoresDosCampos]
        [TratarErros]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Entrar(string login, string senha)
        {
            try
            {
                this._servicoDeLogin.Entrar(new ModeloDeLogin(login, senha, Request.UserHostAddress));
                this.AdicionarMensagemDeSucesso("Login Efetuado com sucesso.");
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
                ViewBag.Mensagem = "Erro ao logar: "+ ex.Message;
            }

            return RedirectToAction(nameof(Index), "Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeUsuario();
            return View(modelo);
        }

        [TratarErros]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Cadastrar(string nome, string email, string senha)
        {
            try
            {
                var modelo = new ModeloDeCadastroDeUsuario(nome, email, senha);
                var retorno = this._servicoDeGestaoDeUsuarios.CadastrarNovoUsuario(modelo);
                this.AdicionarMensagemDeSucesso(retorno);
                ViewBag.Mensagem = "Usuário cadastrado com sucesso. Aguarde contato do Administrador para validação.";
                return View(modelo);
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
                ViewBag.Mensagem = ex.Message;
            }

            return View(new ModeloDeCadastroDeUsuario());
        }

        [HttpGet]
        public ActionResult Sair()
        {
            this._servicoDeLogin.Sair();
            return RedirectToAction(nameof(Index), "Login");
        }
    }
}