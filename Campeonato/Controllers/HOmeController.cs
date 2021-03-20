using Campeonato.Aplicacao.GestaoDeDashboard;
using Campeonato.Aplicacao.GestaoDeDashboard.Modelos;
using Campeonato.Filter;
using Campeonato.SendGrid;
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
    public class HomeController : Controller
    {
        private readonly IServicoDeGestaoDeDashboard _servicoDeGestaoDeDashboard;
        private readonly IServicoDeEnvioDeEmails _servicoDeEnvioDeEmails;
        
        public HomeController(IServicoDeGestaoDeDashboard servicoDeGestaoDeDashboard, IServicoDeEnvioDeEmails servicoDeEnvioDeEmails)
        {
            this._servicoDeGestaoDeDashboard = servicoDeGestaoDeDashboard;
            this._servicoDeEnvioDeEmails = servicoDeEnvioDeEmails;
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.Autenticado())
            {
                var usuario = User.Logado();
                ViewBag.Usuario = usuario.Nome;

                if (User.EhAdministrador())
                {
                    return RedirectToAction(nameof(Dashboard));
                }
            }
            return View();

        }

        [Authorize]
        public ActionResult Dashboard()
        {

            if (User.Autenticado())
            {
                if (User.EhAdministrador())
                {
                    var modelo = new ModeloDeListaDeDashboard();
                    modelo = this._servicoDeGestaoDeDashboard.RetonarDashboardPorFiltro(modelo.Filtro, User.Logado());

                    return View(nameof(Dashboard), modelo);
                }
            }
            return View();

        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}