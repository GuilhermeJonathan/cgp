using Cgp.Aplicacao.GestaoDeEstadio;
using Cgp.Aplicacao.GestaoDeJogos;
using Cgp.Aplicacao.GestaoDeJogos.Modelos;
using Cgp.Aplicacao.GestaoDeRodada;
using Cgp.Aplicacao.GestaoDeTemporadas;
using Cgp.Aplicacao.GestaoDeTimes;
using Cgp.Aplicacao.GestaoDeTimes.Modelos;
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
    [Authorize]
    [TratarErros]
    public class JogoController : Controller
    {
        private readonly IServicoDeGestaoDeJogos _servicoDeGestaoDeJogos;
        private readonly IServicoDeGestaoDeTimes _servicoDeGestaoDeTimes;
        private readonly IServicoDeGestaoDeEstadios _servicoDeGestaoDeEstadios;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        private readonly IServicoDeGestaoDeTemporadas _servicoDeGestaoDeTemporadas;

        public JogoController(IServicoDeGestaoDeJogos servicoDeGestaoDeJogos, IServicoDeGestaoDeTimes servicoDeGestaoDeTimes,
            IServicoDeGestaoDeEstadios servicoDeGestaoDeEstadios, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas, IServicoDeGestaoDeTemporadas servicoDeGestaoDeTemporadas)
        {
            this._servicoDeGestaoDeJogos = servicoDeGestaoDeJogos;
            this._servicoDeGestaoDeTimes = servicoDeGestaoDeTimes;
            this._servicoDeGestaoDeEstadios = servicoDeGestaoDeEstadios;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
            this._servicoDeGestaoDeTemporadas = servicoDeGestaoDeTemporadas;
        }

        public ActionResult Index(ModeloDeListaDeJogos modelo)
        {
            modelo.Filtro.Times = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                      () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

            modelo.Filtro.Temporadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Temporada>(nameof(Temporada.Nome), nameof(Temporada.Id),
                     () => this._servicoDeGestaoDeTemporadas.RetonarTodasAsTemporadasAtivas());

            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                     () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            modelo = this._servicoDeGestaoDeJogos.RetonarTodosOsJogos(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeJogo();

            var temporadaAtiva = this._servicoDeGestaoDeTemporadas.BuscarTemporadaAtiva();
            var rodadaAtiva = this._servicoDeGestaoDeRodadas.BuscarRodadaAtiva();

            if (rodadaAtiva > 0)
                modelo.Rodada = rodadaAtiva;

            if (temporadaAtiva > 0)
                modelo.Temporada = temporadaAtiva;

            modelo.Times1 = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeTimesDaLista>(nameof(ModeloDeTimesDaLista.NomeComSigla), nameof(ModeloDeTimesDaLista.Id),
                       () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesParaSelect());

            modelo.Times2 = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeTimesDaLista>(nameof(ModeloDeTimesDaLista.NomeComSigla), nameof(ModeloDeTimesDaLista.Id),
                       () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesParaSelect());

            modelo.Estadios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Estadio>(nameof(Estadio.Nome), nameof(Estadio.Id),
                     () => this._servicoDeGestaoDeEstadios.RetonarTodosOsEstadiosAtivos());

            modelo.Temporadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Temporada>(nameof(Temporada.Nome), nameof(Temporada.Id),
                    () => this._servicoDeGestaoDeTemporadas.RetonarTodasAsTemporadasAtivas());

            modelo.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                      () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivasPorTemporada(modelo.Temporada));

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeJogo modelo)
        {
            var retorno = this._servicoDeGestaoDeJogos.CadastrarJogo(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                JogoNaoEncontrado();

            var modelo = this._servicoDeGestaoDeJogos.BuscarJogoPorId(id.Value);

            if (modelo.Id == 0)
            {
                JogoNaoEncontrado();
                return RedirectToAction(nameof(Index));
            }

            modelo.Times1 = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                        () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

            modelo.Times2 = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                         () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

            modelo.Estadios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Estadio>(nameof(Estadio.Nome), nameof(Estadio.Id),
                     () => this._servicoDeGestaoDeEstadios.RetonarTodosOsEstadiosAtivos());

            modelo.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                      () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeJogo modelo)
        {
            var retorno = this._servicoDeGestaoDeJogos.AlterarDadosDoJogo(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }
        
        private ActionResult JogoNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("Jogo não encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}