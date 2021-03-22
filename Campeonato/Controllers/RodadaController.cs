using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeRodada.Modelos;
using Campeonato.Aplicacao.GestaoDeTemporadas;
using Campeonato.Aplicacao.GestaoDeTimes;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Dominio.Entidades;
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
    public class RodadaController : Controller
    {
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        private readonly IServicoDeGestaoDeTimes _servicoDeGestaoDeTimes;
        private readonly IServicoDeGestaoDeTemporadas _servicoDeGestaoDeTemporadas;

        public RodadaController(IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas, IServicoDeGestaoDeTimes servicoDeGestaoDeTimes, IServicoDeGestaoDeTemporadas servicoDeGestaoDeTemporadas)
        {
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
            this._servicoDeGestaoDeTimes = servicoDeGestaoDeTimes;
            this._servicoDeGestaoDeTemporadas = servicoDeGestaoDeTemporadas;
        }
        
        [HttpGet]
        public ActionResult Index(ModeloDeListaDeRodadas modelo)
        {
            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                     () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            modelo.Filtro.Temporadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Temporada>(nameof(Temporada.Nome), nameof(Temporada.Id),
                     () => this._servicoDeGestaoDeTemporadas.RetonarTodasAsTemporadasAtivas());
            
            modelo = this._servicoDeGestaoDeRodadas.RetonarTodosasRodadas(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }
            
        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeRodada();

            modelo.Temporadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Temporada>(nameof(Temporada.Nome), nameof(Temporada.Id),
                     () => this._servicoDeGestaoDeTemporadas.RetonarTodasAsTemporadasAtivas());

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeRodada modelo)
        {
            var retorno = this._servicoDeGestaoDeRodadas.CadastrarRodada(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                RodadaNaoEncontrado();

            var modelo = this._servicoDeGestaoDeRodadas.BuscarRodadaPorId(id.Value);

            modelo.Temporadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Temporada>(nameof(Temporada.Nome), nameof(Temporada.Id),
                     () => this._servicoDeGestaoDeTemporadas.RetonarTodasAsTemporadasAtivas());

            return View(modelo);
        }

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

        [HttpGet]
        public ActionResult LancarResultados(int? id)
        {
            if (!id.HasValue)
                RodadaNaoEncontrado();

            var modelo = this._servicoDeGestaoDeRodadas.BuscarRodadaPorId(id.Value);
            return View(modelo);
        }

        [HttpPost]
        public ActionResult LancarResultados(int id, string[] placar1, string[] placar2, int[] idJogos)
        {
            if (User.EhAdministrador() || User.EhInterno())
            {
                if (idJogos != null)
                {
                    var retorno = this._servicoDeGestaoDeRodadas.CadastrarResultados(id, placar1, placar2, idJogos, User.Logado());
                    this.AdicionarMensagemDeSucesso(retorno);
                }
            } else
            {
                this.AdicionarMensagemDeErro("Você não tem permissão para lançar jogos.");
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult FecharRodada(int id)
        {
            var modelo = this._servicoDeGestaoDeRodadas.FecharRodada(id, User.Logado());
            return Content(modelo);
        }

        [HttpGet]
        public JsonResult BuscarRodadasPorTemporada(int temporada)
        {
            var rodadas = this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivasPorTemporada(temporada);
            return Json(new { retorno = rodadas.Select(a => new { Nome = a.Nome, Valor = a.Id }) }, JsonRequestBehavior.AllowGet);
        }
    }
}