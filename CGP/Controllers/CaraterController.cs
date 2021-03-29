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
using System.IO;
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
        private readonly IServicoExternoDeArmazenamentoEmNuvem _servicoExternoDeArmazenamentoEmNuvem;
        private readonly IServicoDeGeracaoDeDocumentosEmPdf _servicoDeGeracaoDeDocumentosEmPdf;

        public CaraterController(IServicoDeGestaoDeCaraters servicoDeGestaoDeCaraters, IServicoDeGestaoDeCidades servicoDeGestaoDeCidades,
            IServicoDeGestaoDeCrimes servicoDeGestaoDeCrimes, IServicoDeBuscaDeVeiculo servicoDeGestaoDeVeiculos, IServicoExternoDeArmazenamentoEmNuvem servicoExternoDeArmazenamentoEmNuvem, 
            IServicoDeGeracaoDeDocumentosEmPdf servicoDeGeracaoDeDocumentosEmPdf)
        {
            this._servicoDeGestaoDeCaraters = servicoDeGestaoDeCaraters;
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
            this._servicoDeGestaoDeCrimes = servicoDeGestaoDeCrimes;
            this._servicoDeGestaoDeVeiculos = servicoDeGestaoDeVeiculos;
            this._servicoExternoDeArmazenamentoEmNuvem = servicoExternoDeArmazenamentoEmNuvem;
            this._servicoDeGeracaoDeDocumentosEmPdf = servicoDeGeracaoDeDocumentosEmPdf;
        }

        public async Task<ActionResult> Index(ModeloDeListaDeCaraters modelo, string exportar)
        {
            if (exportar == "imprimir")
            {
                modelo = this._servicoDeGestaoDeCaraters.GerarPDFeRetornar(modelo.Filtro, User.Logado());

                using (Stream enviarParaAzure = new MemoryStream(this._servicoDeGeracaoDeDocumentosEmPdf.CriarPdf(modelo.ArquivoHtml)))
                {
                    var nomeArquivo = $"caraterGeral{DateTime.Now.ToString().Trim()}.pdf";
                    string blob = $"caraters";
                    var retorno = await this._servicoExternoDeArmazenamentoEmNuvem.EnviarArquivoAsync(enviarParaAzure, blob, nomeArquivo.Trim());
                    Response.Redirect(retorno, true);
                }    
            }

            modelo = this._servicoDeGestaoDeCaraters.RetonarCaratersPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.Cidades = ListaDeItensDeDominio.DaClasseSemOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            modelo.Filtro.Crimes = ListaDeItensDeDominio.DaClasseSemOpcaoPadrao<Crime>(nameof(Crime.Nome), nameof(Crime.Id),
               () => this._servicoDeGestaoDeCrimes.RetonarTodosOsCrimesAtivos());

            if (modelo.Filtro.CidadesSelecionadas != null)
            {
                foreach (var cidade in modelo.Filtro.CidadesSelecionadas)
                    modelo.Filtro.Cidades.FirstOrDefault(a => a.Value == cidade.ToString()).Selected = true;
            }

            if (modelo.Filtro.CrimesSelecionados != null)
            {
                foreach (var crime in modelo.Filtro.CrimesSelecionados)
                    modelo.Filtro.Crimes.FirstOrDefault(a => a.Value == crime.ToString()).Selected = true;
            }

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

        [HttpGet]
        public ActionResult Detalhar(int? id)
        {
            if (!id.HasValue)
            {
                this.AdicionarMensagemDeErro("O caráter não foi encontrado");
                return RedirectToAction(nameof(Index));
            }

            var modelo = this._servicoDeGestaoDeCaraters.BuscarCaraterPorId(id.Value);

            modelo.CidadesLocalizacao = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
              () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            return View(modelo);
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

        [HttpGet]
        public JsonResult BuscarCaraterPorPlaca(string placa)
        {
            var veiculos = this._servicoDeGestaoDeCaraters.BuscarCaraterPorPlaca(placa);
            return Json(new { veiculos.Lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VerificaCadastroDeCarater(string placa)
        {
            var carater = this._servicoDeGestaoDeCaraters.VerificaCadastroDeCarater(placa);
            return Json(new { carater }, JsonRequestBehavior.AllowGet);
        }
    }
}