using Cgp.Aplicacao.GestaoDeBatalhoes;
using Cgp.Aplicacao.GestaoDeBatalhoes.Modelos;
using Cgp.Aplicacao.GestaoDeComandosRegionais;
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
    public class BatalhaoController : Controller
    {
        private readonly IServicoDeGestaoDeBatalhoes _servicoDeGestaoDeBatalhoes;
        private readonly IServicoDeGestaoDeComandosRegionais _servicoDeGestaoDeComandosRegionais;

        public BatalhaoController(IServicoDeGestaoDeBatalhoes servicoDeGestaoDeBatalhoes, IServicoDeGestaoDeComandosRegionais servicoDeGestaoDeComandosRegionais)
        {
            this._servicoDeGestaoDeBatalhoes = servicoDeGestaoDeBatalhoes;
            this._servicoDeGestaoDeComandosRegionais = servicoDeGestaoDeComandosRegionais;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeBatalhoes modelo)
        {
            modelo = this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoes(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.ComandosRegionais = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ComandoRegional>(nameof(ComandoRegional.Sigla), nameof(ComandoRegional.Id),
                    () => this._servicoDeGestaoDeComandosRegionais.RetonarTodosOsComandosRegionaisAtivos());

            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeBatalhao();

            modelo.ComandosRegionais = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ComandoRegional>(nameof(ComandoRegional.Sigla), nameof(ComandoRegional.Id),
                () => this._servicoDeGestaoDeComandosRegionais.RetonarTodosOsComandosRegionaisAtivos());

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeBatalhao modelo)
        {
            var retorno = this._servicoDeGestaoDeBatalhoes.CadastrarBatalhao(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                TimeNaoEncontrado();

            var modelo = this._servicoDeGestaoDeBatalhoes.BuscarBatalhaoPorId(id.Value);

            modelo.ComandosRegionais = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ComandoRegional>(nameof(ComandoRegional.Sigla), nameof(ComandoRegional.Id),
                () => this._servicoDeGestaoDeComandosRegionais.RetonarTodosOsComandosRegionaisAtivos());

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeBatalhao modelo)
        {
            var retorno = this._servicoDeGestaoDeBatalhoes.AlterarDadosDoTime(modelo, User.Logado());

            modelo.ComandosRegionais = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ComandoRegional>(nameof(ComandoRegional.Sigla), nameof(ComandoRegional.Id),
                () => this._servicoDeGestaoDeComandosRegionais.RetonarTodosOsComandosRegionaisAtivos());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult TimeNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O batalhão não foi encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}