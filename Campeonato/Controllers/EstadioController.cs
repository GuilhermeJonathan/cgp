using Cgp.Aplicacao.GestaoDeEstadio;
using Cgp.Aplicacao.GestaoDeEstadio.Modelos;
using Cgp.Aplicacao.GestaoDeTimes;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    public class EstadioController : Controller
    {
        private readonly IServicoDeGestaoDeEstadios _servicoDeGestaoDeEstadios;
        private readonly IServicoDeGestaoDeTimes _servicoDeGestaoDeTimes;

        public EstadioController(IServicoDeGestaoDeEstadios servicoDeGestaoDeEstadios, IServicoDeGestaoDeTimes servicoDeGestaoDeTimes)
        {
            this._servicoDeGestaoDeEstadios = servicoDeGestaoDeEstadios;
            this._servicoDeGestaoDeTimes = servicoDeGestaoDeTimes;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(ModeloDeListaDeEstadios modelo)
        {

            modelo.Filtro.Times = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                         () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

            modelo = this._servicoDeGestaoDeEstadios.RetonarTodosOsEstadios(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeEstadio();

            modelo.Times = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                       () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeEstadio modelo)
        {
            var retorno = this._servicoDeGestaoDeEstadios.CadastrarEstadio(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                EstadioNaoEncontrado();

            var modelo = this._servicoDeGestaoDeEstadios.BuscarEstadioPorId(id.Value);

            modelo.Times = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Time>(nameof(Time.Nome), nameof(Time.Id),
                         () => this._servicoDeGestaoDeTimes.RetonarTodosOsTimesAtivos());

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeEstadio modelo)
        {
            var retorno = this._servicoDeGestaoDeEstadios.AlterarDadosDoEstadio(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult EstadioNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O estádio não foi encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}