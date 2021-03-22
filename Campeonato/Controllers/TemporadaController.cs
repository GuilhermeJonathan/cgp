using Campeonato.Aplicacao.GestaoDeTemporadas;
using Campeonato.Aplicacao.GestaoDeTemporadas.Modelos;
using Campeonato.Aplicacao.Util;
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
    [Authorize]
    [TratarErros]
    public class TemporadaController : Controller
    {
        private readonly IServicoDeGestaoDeTemporadas _servicoDeGestaoDeTemporada;

        public TemporadaController(IServicoDeGestaoDeTemporadas servicoDeGestaoDeTemporada)
        {
            this._servicoDeGestaoDeTemporada = servicoDeGestaoDeTemporada;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeTemporada modelo)
        {
            modelo = this._servicoDeGestaoDeTemporada.RetornarTemporadasPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeTemporada();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeTemporada modelo)
        {
            var retorno = this._servicoDeGestaoDeTemporada.CadastrarTemporada(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                TemporadaNaoEncontrado();

            var modelo = this._servicoDeGestaoDeTemporada.BuscarTemporadaPorId(id.Value);

            if (modelo.Id == 0)
            {
                TemporadaNaoEncontrado();
                return RedirectToAction(nameof(Index));
            }

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeTemporada modelo)
        {
            var retorno = this._servicoDeGestaoDeTemporada.AlterarDadosDaTemporada(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AtivarTemporada(int id)
        {
            var modelo = this._servicoDeGestaoDeTemporada.AtivarTemporada(id, User.Logado());
            return Content(modelo);
        }

        private ActionResult TemporadaNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("Temporada não encontrada");
            return RedirectToAction(nameof(Index));
        }
    }
}