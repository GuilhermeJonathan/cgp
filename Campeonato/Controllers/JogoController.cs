using Campeonato.Aplicacao.GestaoDeEstadio;
using Campeonato.Aplicacao.GestaoDeJogos;
using Campeonato.Aplicacao.GestaoDeJogos.Modelos;
using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeTimes;
using Campeonato.Aplicacao.GestaoDeTimes.Modelos;
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
    public class JogoController : Controller
    {
        private readonly IServicoDeGestaoDeJogos _servicoDeGestaoDeJogos;
        private readonly IServicoDeGestaoDeTimes _servicoDeGestaoDeTimes;
        private readonly IServicoDeGestaoDeEstadios _servicoDeGestaoDeEstadios;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;

        public JogoController(IServicoDeGestaoDeJogos servicoDeGestaoDeJogos, IServicoDeGestaoDeTimes servicoDeGestaoDeTimes,
            IServicoDeGestaoDeEstadios servicoDeGestaoDeEstadios, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas)
        {
            this._servicoDeGestaoDeJogos = servicoDeGestaoDeJogos;
            this._servicoDeGestaoDeTimes = servicoDeGestaoDeTimes;
            this._servicoDeGestaoDeEstadios = servicoDeGestaoDeEstadios;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
        }

        public ActionResult Index(ModeloDeListaDeJogos modelo)
        {
            modelo.Filtro.Times = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                      () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

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

            modelo.Times1 = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeTimesDaLista>(nameof(ModeloDeTimesDaLista.NomeComSigla), nameof(ModeloDeTimesDaLista.Id),
                       () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesParaSelect());

            modelo.Times2 = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeTimesDaLista>(nameof(ModeloDeTimesDaLista.NomeComSigla), nameof(ModeloDeTimesDaLista.Id),
                       () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesParaSelect());

            modelo.Estadios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Estadio>(nameof(Estadio.Nome), nameof(Estadio.Id),
                     () => this._servicoDeGestaoDeEstadios.RetonarTodosOsEstadiosAtivos());

            modelo.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                      () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            var rodadaAtiva = this._servicoDeGestaoDeRodadas.BuscarRodadaAtiva();

            if (rodadaAtiva > 0)
                modelo.Rodada = rodadaAtiva;

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