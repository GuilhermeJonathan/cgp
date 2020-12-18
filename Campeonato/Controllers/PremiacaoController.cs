using Campeonato.Aplicacao.GestaoDePremiacoes;
using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
{
    [TratarErros]
    [Authorize]
    public class PremiacaoController : Controller
    {
        
        private readonly IServicoDeGestaoDePremiacoes _servicoDeGestaoDePremiacoes;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;
        public PremiacaoController(IServicoDeGestaoDePremiacoes servicoDeGestaoDePremiacoes, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeGestaoDePremiacoes = servicoDeGestaoDePremiacoes;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        public ActionResult Index()
        {
            return View();
        }

        [TratarErros]
        [HttpGet]
        public ActionResult GerarPremiacao(int? id)
        {
            if (!id.HasValue)
                RodadaNaoEncontrada();

            return RedirectToAction(nameof(Index));
        }

        private ActionResult RodadaNaoEncontrada()
        {
            this.AdicionarMensagemDeErro("Rodada não foi encontrada");
            return RedirectToAction(nameof(Index));
        }
    }
}