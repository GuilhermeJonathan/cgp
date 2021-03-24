using Cgp.Aplicacao.GestaoDeComandosRegionais;
using Cgp.Aplicacao.GestaoDeComandosRegionais.Modelos;
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
    public class ComandoRegionalController : Controller
    {
        private readonly IServicoDeGestaoDeComandosRegionais _servicoDeGestaoDeComandosRegionais;
        public ComandoRegionalController(IServicoDeGestaoDeComandosRegionais servicoDeGestaoDeComandosRegionais)
        {
            this._servicoDeGestaoDeComandosRegionais = servicoDeGestaoDeComandosRegionais;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeComandosRegionais modelo)
        {
            modelo = this._servicoDeGestaoDeComandosRegionais.RetonarComandosRegionaisPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeComandoRegional();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeComandoRegional modelo)
        {
            var retorno = this._servicoDeGestaoDeComandosRegionais.CadastrarComandoRegional(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                ComandoNaoEncontrado();

            var modelo = this._servicoDeGestaoDeComandosRegionais.BuscarComandoRegionalPorId(id.Value);

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeComandoRegional modelo)
        {
            var retorno = this._servicoDeGestaoDeComandosRegionais.AlterarDadosDoComandoRegional(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AtivarComando(int id)
        {
            var modelo = this._servicoDeGestaoDeComandosRegionais.AtivarComando(id, User.Logado());
            return Content(modelo);
        }

        private ActionResult ComandoNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O comando regional não foi encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}