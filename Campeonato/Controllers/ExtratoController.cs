using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.CustomExtensions;
using Campeonato.Filter;
using Campeonato.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
{
    public class ExtratoController : Controller
    {
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;
        public ExtratoController(IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        [TratarErros]
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var usuario = User.Logado();
                var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioComHistoricoPorId(usuario.Id);
                this.TotalDeRegistrosEncontrados(modelo.HistoricosFinanceiros.Count);
                return View(modelo);
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public ActionResult RetirarSaldo(int Saldo, int TipoDePix, string ChavePix)
        {
            try
            {
                var retorno = this._servicoDeGestaoDeUsuarios.RetirarSaldo(Saldo, User.Logado(), TipoDePix, ChavePix);
                this.AdicionarMensagemDeSucesso(retorno);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}