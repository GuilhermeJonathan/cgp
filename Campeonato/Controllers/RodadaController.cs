using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeRodada.Modelos;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
{
    public class RodadaController : Controller
    {

        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;

        public RodadaController(IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas)
        {
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(ModeloDeListaDeRodadas modelo)
        {
            modelo = this._servicoDeGestaoDeRodadas.RetonarTodosasRodadas(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeRodada();
            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeRodada modelo)
        {
            var retorno = this._servicoDeGestaoDeRodadas.CadastrarRodada(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                RodadaNaoEncontrado();

            var modelo = this._servicoDeGestaoDeRodadas.BuscarRodadaPorId(id.Value);
            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeRodada modelo)
        {
            var retorno = this._servicoDeGestaoDeRodadas.AlterarDadosDaRodada(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult RodadaNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("Rodada não foi encontrada");
            return RedirectToAction(nameof(Index));
        }
    }
}