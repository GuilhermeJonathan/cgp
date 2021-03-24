using Cgp.Aplicacao.GestaoDeDashboard;
using Cgp.Aplicacao.GestaoDeDashboard.Modelos;
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
                ViewBag.Usuario = User.Logado().Nome;

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