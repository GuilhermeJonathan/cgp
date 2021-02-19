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
using System.IO;
using System.Web.Routing;
using Campeonato.Dominio.ObjetosDeValor;
using WkHtmlToXSharp;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Configuration;

namespace Campeonato.Controllers
{
    [TratarErros]
    [Authorize]
    public class ApostaController : Controller
    {
        private readonly IServicoDeGestaoDeApostas _servicoDeGestaoDeApostas;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;
        private readonly IServicoExternoDeArmazenamentoEmNuvem _servicoExternoDeArmazenamentoEmNuvem;
        private readonly IServicoDeGeracaoDeDocumentosEmPdf _servicoDeGeracaoDeDocumentosEmPdf;

        public ApostaController(IServicoDeGestaoDeApostas servicoDeGestaoDeApostas, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas,
            IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios, IServicoExternoDeArmazenamentoEmNuvem servicoExternoDeArmazenamentoEmNuvem,
            IServicoDeGeracaoDeDocumentosEmPdf servicoDeGeracaoDeDocumentosEmPdf)
        {
            this._servicoDeGestaoDeApostas = servicoDeGestaoDeApostas;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
            this._servicoExternoDeArmazenamentoEmNuvem = servicoExternoDeArmazenamentoEmNuvem;
            this._servicoDeGeracaoDeDocumentosEmPdf = servicoDeGeracaoDeDocumentosEmPdf;
            ConfigurarGlobalizacaoParaPortugues();
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

                modelo.Filtro.TipoDeAposta = TipoDeAposta.Geral;
                modelo = this._servicoDeGestaoDeApostas.BuscarApostasPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
                this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);

                if (modelo.Filtro.Rodada == 0)
                    modelo.Filtro.RodadaParaLink = rodadaAtiva;
                else
                    modelo.Filtro.RodadaParaLink = modelo.Filtro.Rodada;

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
        [HttpGet]
        public async Task<ActionResult> VisualizarTodasApostas(int? id, int? idUsuario)
        {
            if (!id.HasValue)
                ApostaNaoEncontrada();

            var modelo = this._servicoDeGestaoDeApostas.BuscarTodasApostasPorRodada(id.Value, User.Logado());

            //if (!modelo.TemArquivo)
            //{
                using (Stream enviarParaAzure = new MemoryStream(this._servicoDeGeracaoDeDocumentosEmPdf.CriarPdf(modelo.ArquivoHtml)))
                {
                    var nomeArquivo = $"espelhoDa{modelo.NomeRodada.Trim()}.pdf";
                    string blob = $"espelhos";
                    var retorno = await this._servicoExternoDeArmazenamentoEmNuvem.EnviarArquivoAsync(enviarParaAzure, blob, nomeArquivo.Trim());
                    this._servicoDeGestaoDeApostas.SalvarArquivoDaRodada(modelo.RodadaId, retorno);
                }
            //}

            modelo.Filtro.Rodada = id.Value;
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

        public class FakeController : ControllerBase
        {
            protected override void ExecuteCore() { }
            public static string RenderViewToString(string controllerName, string viewName, object model)
            {
                using (var writer = new StringWriter())
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controllerName);
                    var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://localhost", null), new HttpResponse(null))), routeData, new FakeController());
                    var razorViewEngine = new RazorViewEngine();
                    var razorViewResult = razorViewEngine.FindPartialView(fakeControllerContext, viewName, false);
                    var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(model), new TempDataDictionary(), writer);
                    razorViewResult.View.Render(viewContext, writer);
                    return writer.ToString();

                }
            }
        }

        private static void ConfigurarGlobalizacaoParaPortugues()
        {
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

    }
}