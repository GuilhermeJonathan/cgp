using Campeonato.Aplicacao.GestaoDeTimes;
using Campeonato.Aplicacao.GestaoDeTimes.Modelos;
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
    public class TimeController : Controller
    {
        private readonly IServicoDeGestaoDeTimes _servicoDeGestaoDeTimes;

        public TimeController(IServicoDeGestaoDeTimes servicoDeGestaoDeTimes)
        {
            this._servicoDeGestaoDeTimes = servicoDeGestaoDeTimes;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeTimes modelo)
        {
            modelo = this._servicoDeGestaoDeTimes.RetonarTodosOsTimes(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeTime();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeTime modelo)
        {
            var retorno = this._servicoDeGestaoDeTimes.CadastrarTime(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                TimeNaoEncontrado();

            var modelo = this._servicoDeGestaoDeTimes.BuscarTimePorId(id.Value);
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeTime modelo)
        {
            var retorno = this._servicoDeGestaoDeTimes.AlterarDadosDoTime(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult TimeNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O time não foi encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}