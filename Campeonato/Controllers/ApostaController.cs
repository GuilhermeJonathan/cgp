using Campeonato.Aplicacao.GestaoDeApostas.Modelos;
using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
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
    [TratarErros]
    [Authorize]
    public class ApostaController : Controller
    {
        private readonly IServicoDeGestaoDeApostas _servicoDeGestaoDeApostas;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public ApostaController(IServicoDeGestaoDeApostas servicoDeGestaoDeApostas, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas,
            IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeGestaoDeApostas = servicoDeGestaoDeApostas;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        public ActionResult Index(ModeloDeListaDeApostas modelo)
        {
            var rodadaAtiva = this._servicoDeGestaoDeRodadas.BuscarRodadaAtiva();


            if (User.EhAdministrador())
            {
                modelo.Filtro.Usuarios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeUsuarioDaLista>(nameof(ModeloDeUsuarioDaLista.Nome), nameof(ModeloDeUsuarioDaLista.Id),
                        () => this._servicoDeGestaoDeUsuarios.RetonarTodosOsUsuariosAtivos());

                modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                        () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

                modelo = this._servicoDeGestaoDeApostas.BuscarApostasPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
                this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);

                if (rodadaAtiva > 0)
                    modelo.Filtro.Rodada = rodadaAtiva;

                return View(modelo);
            }
            else
            {
                if (rodadaAtiva > 0)
                    modelo.Filtro.Rodada = rodadaAtiva;

                return RedirectToAction(nameof(MinhasApostas), new { id = modelo.Filtro.Rodada });
            }
        }

        [TratarErros]
        [HttpGet]
        public ActionResult MinhasApostas(int? id)
        {
            if (!id.HasValue)
                id = this._servicoDeGestaoDeRodadas.BuscarRodadaAtiva();

            var modelo = this._servicoDeGestaoDeApostas.BuscarApostaPorRodada(id.Value, User.Logado());

            modelo.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                   () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Visualizar(int? id, int? idUsuario)
        {
            if (!id.HasValue)
                ApostaNaoEncontrada();

            var modelo = this._servicoDeGestaoDeApostas.VisualizarAposta(id.Value, idUsuario.Value);
            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SalvarMinhaAposta(int id, int[] placar1, int[] placar2, int[] idJogos)
        {
            if (idJogos != null)
            {
                var retorno = this._servicoDeGestaoDeApostas.SalvarMinhaAposta(id, placar1, placar2, idJogos, User.Logado());
                this.AdicionarMensagemDeSucesso(retorno);
            }

            if (!User.EhAdministrador())
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(MinhasApostas), new { id = id });
        }

        [TratarErros]
        [Authorize]
        [HttpPost]
        public ActionResult GerarApostaExclusiva(int? Id, int IdRodada, int Usuario)
        {
            if (!Id.HasValue)
                ApostaNaoEncontrada();

            if (Id != null)
            {
                var retorno = this._servicoDeGestaoDeApostas.GerarApostaExclusiva(Id.Value, IdRodada, Usuario, User.Logado());
                this.AdicionarMensagemDeSucesso(retorno);
            }

            if (!User.EhAdministrador())
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(MinhasApostas), new { id = Id });
        }

        private ActionResult ApostaNaoEncontrada()
        {
            this.AdicionarMensagemDeErro("Aposta não foi encontrada");
            return RedirectToAction(nameof(Index));
        }

    }
}