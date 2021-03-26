using Cgp.Aplicacao.BuscaVeiculo;
using Cgp.Aplicacao.GestaoDeCaraters;
using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Aplicacao.GestaoDeCidades;
using Cgp.Aplicacao.GestaoDeCrimes;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Filter;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    [Authorize]
    [TratarErros]
    public class CaraterController : Controller
    {
        private readonly IServicoDeGestaoDeCaraters _servicoDeGestaoDeCaraters;
        private readonly IServicoDeGestaoDeCidades _servicoDeGestaoDeCidades;
        private readonly IServicoDeGestaoDeCrimes _servicoDeGestaoDeCrimes;
        private readonly IServicoDeBuscaDeVeiculo _servicoDeGestaoDeVeiculos;
        private readonly IServicoDeGeracaoDeDocumentosEmPdf _servicoDeGeracaoDeDocumentosEmPdf;

        public CaraterController(IServicoDeGestaoDeCaraters servicoDeGestaoDeCaraters, IServicoDeGestaoDeCidades servicoDeGestaoDeCidades,
            IServicoDeGestaoDeCrimes servicoDeGestaoDeCrimes, IServicoDeBuscaDeVeiculo servicoDeGestaoDeVeiculos, IServicoDeGeracaoDeDocumentosEmPdf servicoDeGeracaoDeDocumentosEmPdf)
        {
            this._servicoDeGestaoDeCaraters = servicoDeGestaoDeCaraters;
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
            this._servicoDeGestaoDeCrimes = servicoDeGestaoDeCrimes;
            this._servicoDeGestaoDeVeiculos = servicoDeGestaoDeVeiculos;
            this._servicoDeGeracaoDeDocumentosEmPdf = servicoDeGeracaoDeDocumentosEmPdf;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeCaraters modelo)
        {
            modelo = this._servicoDeGestaoDeCaraters.RetonarCaratersPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            modelo.Filtro.Crimes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Crime>(nameof(Crime.Nome), nameof(Crime.Id),
               () => this._servicoDeGestaoDeCrimes.RetonarTodosOsCrimesAtivos());

            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeCarater();

            modelo.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            modelo.Crimes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Crime>(nameof(Crime.Nome), nameof(Crime.Id),
              () => this._servicoDeGestaoDeCrimes.RetonarTodosOsCrimesAtivos());

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeCarater modelo)
        {
            var retorno = this._servicoDeGestaoDeCaraters.CadastrarCarater(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
            {
                this.AdicionarMensagemDeErro("O caráter não foi encontrado");
                return RedirectToAction(nameof(Index));
            }

            var modelo = this._servicoDeGestaoDeCaraters.BuscarCaraterPorId(id.Value);

            modelo.Crimes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Crime>(nameof(Crime.Nome), nameof(Crime.Id),
             () => this._servicoDeGestaoDeCrimes.RetonarTodosOsCrimesAtivos());

            modelo.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeCarater modelo)
        {
            var retorno = this._servicoDeGestaoDeCaraters.AlterarDadosDoCarater(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        private ActionResult CaraterNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("O caráter não foi encontrado");
            return RedirectToAction(nameof(Index));   
        }

        [HttpGet]
        public async Task<JsonResult> BuscarPlacaDoVeiculo(string placa)
        {
            var veiculo = await this._servicoDeGestaoDeVeiculos.BuscarPlacaSimples(placa);
            return Json(new { veiculo }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RealizarBaixa(int? idCarater, string descricao, int? cidade)
        {
            var resultado = this._servicoDeGestaoDeCaraters.RealizarBaixaVeiculo(idCarater.Value, descricao, cidade.Value, User.Logado());
            this.AdicionarMensagemDeSucesso(resultado);
            return Json(new { resultado }, JsonRequestBehavior.AllowGet);
        }
    }
}