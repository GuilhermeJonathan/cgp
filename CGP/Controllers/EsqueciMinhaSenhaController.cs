using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Login;
using Cgp.CustomExtensions;
using Cgp.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    [TratarErros]
    [RoutePrefix("EsqueciMinhaSenha")]
    public class EsqueciMinhaSenhaController : Controller
    {
        private readonly IServicoDeLogin _servicoDeLogin;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public EsqueciMinhaSenhaController(IServicoDeLogin servicoDeLogin, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeLogin = servicoDeLogin;
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
        public async Task<ActionResult> EsqueciMinhaSenha(string login)
        {
            try
            {
                var retorno = await this._servicoDeLogin.EnviarEmailEsqueciMinhaSenha(login);
                this.AdicionarMensagemDeSucesso(retorno);

                ViewBag.Mensagem = retorno.ToString();
                return RedirectToAction(nameof(Index),"Login");
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
                ViewBag.Mensagem = "Erro ao enviar solicitação: " + ex.Message;
            }

            return RedirectToAction(nameof(Index), "Login");
        }

        
        [TratarErros]
        [HttpGet]
        [Route("RenovacaoDeSenha/{t}")]
        public ActionResult RenovacaoDeSenha(string t)
        {
            try
            {
                var retorno = this._servicoDeLogin.ValidarTokenRetornarUsuario(t);
                if (retorno != null)
                    return View(retorno);
                else
                {
                    this.AdicionarMensagemDeErro("Usuário não encontrado.");
                    RedirectToAction(nameof(Index), "Login");
                }
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro("Token Inválido.");
                ViewBag.Mensagem = "Erro ao validar token: " + ex.Message;
                RedirectToAction(nameof(Index), "Login");
            }

            return RedirectToAction(nameof(Index), "Login");
        }

        [TratarErros]
        [HttpPost]
        public ActionResult AlterarSenhaRenovacao(ModeloDeEdicaoDeUsuario modelo)
        {
            var retorno = this._servicoDeGestaoDeUsuarios.AlterarSenhaRenovacao(modelo);
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index), "Login");
        }
    }
}