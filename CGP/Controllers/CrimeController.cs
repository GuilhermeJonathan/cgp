using Cgp.Aplicacao.GestaoDeCrimes;
using Cgp.Aplicacao.GestaoDeCrimes.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
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
    public class CrimeController : Controller
    {
        private readonly IServicoDeGestaoDeCrimes _servicoDeGestaoDeCrimes;
        public CrimeController(IServicoDeGestaoDeCrimes servicoDeGestaoDeCrimes)
        {
            this._servicoDeGestaoDeCrimes = servicoDeGestaoDeCrimes;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeCrimes modelo)
        {
            modelo = this._servicoDeGestaoDeCrimes.RetonarCrimesPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeCrime();

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeCrime modelo)
        {
            var retorno = this._servicoDeGestaoDeCrimes.CadastrarCrime(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                CrimeNaoEncontrado();

            var modelo = this._servicoDeGestaoDeCrimes.BuscarCrimePorId(id.Value);

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeCrime modelo)
        {
            var retorno = this._servicoDeGestaoDeCrimes.AlterarDadosDoCrime(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AtivarCrime(int id)
        {
            var modelo = this._servicoDeGestaoDeCrimes.AtivarCrime(id, User.Logado());
            return Content(modelo);
        }

        private ActionResult CrimeNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O crime não foi encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}