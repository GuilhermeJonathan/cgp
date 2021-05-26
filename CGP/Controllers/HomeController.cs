using Cgp.Aplicacao.GestaoDeCaraters;
using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Aplicacao.GestaoDeCidades;
using Cgp.Aplicacao.GestaoDeCrimes;
using Cgp.Aplicacao.GestaoDeDashboard;
using Cgp.Aplicacao.GestaoDeDashboard.Modelos;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Filter;
using Cgp.SendGrid;
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
    public class HomeController : Controller
    {
        private readonly IServicoDeGestaoDeDashboard _servicoDeGestaoDeDashboard;
        private readonly IServicoDeGestaoDeCaraters _servicoDeGestaoDeCaraters;
        private readonly IServicoDeGestaoDeCidades _servicoDeGestaoDeCidades;
        private readonly IServicoDeGestaoDeCrimes _servicoDeGestaoDeCrimes;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public HomeController(IServicoDeGestaoDeDashboard servicoDeGestaoDeDashboard, IServicoDeGestaoDeCaraters servicoDeGestaoDeCaraters, IServicoDeGestaoDeCidades servicoDeGestaoDeCidades,
            IServicoDeGestaoDeCrimes servicoDeGestaoDeCrimes, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeGestaoDeDashboard = servicoDeGestaoDeDashboard;
            this._servicoDeGestaoDeCaraters = servicoDeGestaoDeCaraters;
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
            this._servicoDeGestaoDeCrimes = servicoDeGestaoDeCrimes;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.Autenticado())
            {
                ViewBag.Usuario = User.Logado().Nome;
                ViewBag.Tutorial = $"{VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob")}/arquivos/TutorialCaraterGeral.pdf";
                return RedirectToAction(nameof(Dashboard));
            }

            return View();
        }

        [Authorize]
        public ActionResult Dashboard(ModeloDeListaDeCaraters modelo)
        {
            if (User.Autenticado())
            {
                modelo = this._servicoDeGestaoDeCaraters.RetonarCaratersPorCidades(modelo.Filtro);

                modelo.Filtro.Cidades = ListaDeItensDeDominio.DaClasseSemOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
                () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

                modelo.Filtro.CidadesLocalizacao = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
                () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

                if (modelo.Filtro.CidadesSelecionadas != null)
                {
                    foreach (var cidade in modelo.Filtro.CidadesSelecionadas)
                        modelo.Filtro.Cidades.FirstOrDefault(a => a.Value == cidade.ToString()).Selected = true;
                }

                return View(nameof(Dashboard), modelo);
            }
            return View();
        }

        [HttpPost]
        public ActionResult DarCiencia()
        {
            this._servicoDeGestaoDeUsuarios.DarCienciaEmTermo(User.Logado());
            this.AdicionarMensagemDeSucesso("Ciência realizada com sucesso.");
            return RedirectToAction(nameof(Dashboard));   
        }

        [HttpGet]
        public JsonResult VerificarCiencia()
        {
            bool resultado = true;
            bool ciencia = Session["ciencia"] != null ? Convert.ToBoolean(Session["ciencia"]) : false;

            if (ciencia)
                return Json(new { resultado }, JsonRequestBehavior.AllowGet);

            resultado = this._servicoDeGestaoDeUsuarios.VerificarCiencia(User.Logado());
            Session.Add("ciencia", resultado);
            return Json(new { resultado }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}