using Campeonato.Aplicacao.GestaoDeApostas.Modelos;
using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
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
    public class ApostaExclusivaController : Controller
    {
        private readonly IServicoDeGestaoDeApostas _servicoDeGestaoDeApostas;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public ApostaExclusivaController(IServicoDeGestaoDeApostas servicoDeGestaoDeApostas, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas,
            IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeGestaoDeApostas = servicoDeGestaoDeApostas;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        [Route("{id}")]
        [TratarErros]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var idRodada = 0;

            if (!id.HasValue)
                idRodada = this._servicoDeGestaoDeRodadas.BuscarRodadaAtiva();
            else
                idRodada = id.Value;

            var modelo = new ModeloDeListaDeApostas();
            modelo.Filtro.Rodada = idRodada;
            
            modelo.Filtro.Usuarios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeUsuarioDaLista>(nameof(ModeloDeUsuarioDaLista.Nome), nameof(ModeloDeUsuarioDaLista.Id),
                    () => this._servicoDeGestaoDeUsuarios.RetonarTodosOsUsuariosAtivos());

            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                    () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());
            modelo.Filtro.TipoDeAposta = TipoDeAposta.Exclusiva;

            modelo = this._servicoDeGestaoDeApostas.BuscarApostasPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);

            if (idRodada > 0)
                modelo.Filtro.Rodada = idRodada;

            return View(modelo);
           
        }

        [Authorize]
        [HttpGet]
        public ActionResult Visualizar(int? idAposta, int? idUsuario)
        {
            if (!idAposta.HasValue)
                ApostaNaoEncontrada();

            var modelo = this._servicoDeGestaoDeApostas.VisualizarApostaExclusiva(idAposta.Value, idUsuario.Value);
            return View(modelo);
        }

        private ActionResult ApostaNaoEncontrada()
        {
            this.AdicionarMensagemDeErro("Aposta não foi encontrada");
            return RedirectToAction(nameof(Index));
        }
    }
}