using Cgp.Aplicacao.GestaoDeBatalhoes;
using Cgp.Aplicacao.GestaoDeBatalhoes.Modelos;
using Cgp.Aplicacao.GestaoDeCidades;
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
        private readonly IServicoDeGestaoDeCidades _servicoDeGestaoDeCidades;

        public BatalhaoController(IServicoDeGestaoDeBatalhoes servicoDeGestaoDeBatalhoes, IServicoDeGestaoDeComandosRegionais servicoDeGestaoDeComandosRegionais, IServicoDeGestaoDeCidades servicoDeGestaoDeCidades)
        {
            this._servicoDeGestaoDeBatalhoes = servicoDeGestaoDeBatalhoes;
            this._servicoDeGestaoDeComandosRegionais = servicoDeGestaoDeComandosRegionais;
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeBatalhoes modelo)
        {
            modelo = this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoes(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.ComandosRegionais = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ComandoRegional>(nameof(ComandoRegional.Sigla), nameof(ComandoRegional.Id),
                    () => this._servicoDeGestaoDeComandosRegionais.RetonarTodosOsComandosRegionaisAtivos());

            modelo.Filtro.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeBatalhao();

            modelo.ComandosRegionais = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ComandoRegional>(nameof(ComandoRegional.Sigla), nameof(ComandoRegional.Id),
                () => this._servicoDeGestaoDeComandosRegionais.RetonarTodosOsComandosRegionaisAtivos());

            modelo.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

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

            modelo.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeBatalhao modelo)
        {
            var retorno = this._servicoDeGestaoDeBatalhoes.AlterarDadosDoBatalhao(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AtivarBatalhao(int id)
        {
            var modelo = this._servicoDeGestaoDeBatalhoes.AtivarBatalhao(id, User.Logado());
            return Content(modelo);
        }

        private ActionResult TimeNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O batalhão não foi encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}