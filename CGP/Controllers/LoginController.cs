using Cgp.Aplicacao;
using Cgp.Aplicacao.GestaoDeBatalhoes;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Login;
using Cgp.Aplicacao.Login.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Filter;
using Cgp.SendGrid;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IServicoDeEnvioDeEmails _servicoDeEnvioDeEmails;

        public LoginController(IServicoDeLogin servicoDeLogin, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios, IServicoDeGestaoDeBatalhoes servicoDeGestaoDeBatalhoes, IServicoDeEnvioDeEmails servicoDeEnvioDeEmails)
        {
            _servicoDeLogin = servicoDeLogin;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
            this._servicoDeGestaoDeBatalhoes = servicoDeGestaoDeBatalhoes;
            this._servicoDeEnvioDeEmails = servicoDeEnvioDeEmails;
        }
       
        public ActionResult Index()
        {
            return View();
        }

        [RecuperarValoresDosCampos]
        [TratarErros]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Entrar(string login, string senha)
        {
            try
            {
                await this._servicoDeLogin.EntrarAsync(new ModeloDeLogin(login, senha, Request.UserHostAddress));
                this.AdicionarMensagemDeSucesso("Login Efetuado com sucesso.");
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro ao logar: " + ex.Message;
                throw new ExcecaoDeAplicacao(ex.Message);
            }
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
        public async Task<ActionResult> Cadastrar(string nome, string email, string senha, string matricula, int? batalhao)
        {
            try
            {
                var modelo = new ModeloDeCadastroDeUsuario(nome, email, senha, matricula, batalhao.HasValue ? batalhao.Value : 0);
                var retorno = await this._servicoDeGestaoDeUsuarios.CadastrarNovoUsuario(modelo);
                this.AdicionarMensagemDeSucesso(retorno);
                ViewBag.Mensagem = "Usuário cadastrado com sucesso. Você receberá um email com as orientações.";

                modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoParametro<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos(), "Selecione o batalhão");

                return View(modelo);
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
                ViewBag.Mensagem = $"{ex.Message}. Se já realizou o cadastro, aguarde validação do administrador.";
            }

            return View(new ModeloDeCadastroDeUsuario());
        }

        [HttpGet]
        public ActionResult Sair()
        {
            this._servicoDeLogin.Sair();
            Session.Clear();
            return RedirectToAction(nameof(Index), "Login");
        }
    }
}