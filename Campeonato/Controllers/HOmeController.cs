using Campeonato.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
{
    public class HomeController : Controller
    {
        // GET: HOme
        public ActionResult Index()
        {
            if (User.Autenticado())
            {
                var usuario = User.Logado();
                ViewBag.Usuario = usuario.Nome;
            }
            return View();

        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}