using Cgp.Aplicacao.GestaoDeBatalhoes;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Login;
using Cgp.Aplicacao.Login.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
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
        private readonly IServicoDeGestaoDeBatalhoes _servicoDeGestaoDeBatalhoes;

        public LoginController(IServicoDeLogin servicoDeLogin, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios, IServicoDeGestaoDeBatalhoes servicoDeGestaoDeBatalhoes)
        {
            _servicoDeLogin = servicoDeLogin;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
            this._servicoDeGestaoDeBatalhoes = servicoDeGestaoDeBatalhoes;
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

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoParametro<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos(), "Selecione o batalhão");

            return View(modelo);
        }

        [TratarErros]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Cadastrar(string nome, string email, string senha, string matricula, int? batalhao)
        {
            try
            {
                var modelo = new ModeloDeCadastroDeUsuario(nome, email, senha, matricula, batalhao.HasValue ? batalhao.Value : 0);
                var retorno = this._servicoDeGestaoDeUsuarios.CadastrarNovoUsuario(modelo);
                this.AdicionarMensagemDeSucesso(retorno);
                ViewBag.Mensagem = "Usuário cadastrado com sucesso. Aguarde contato do Administrador para validação.";

                modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoParametro<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos(), "Selecione o batalhão");

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