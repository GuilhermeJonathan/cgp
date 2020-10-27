using GCN.Aplicacao.GestaoDeFuncionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCN.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicoDeGestaoDeFuncionarios _servicoDeGestaoDeClientes;
        

        public HomeController(IServicoDeGestaoDeFuncionarios servicoDeGestaoDeClientes)
        {
            this._servicoDeGestaoDeClientes = servicoDeGestaoDeClientes;
        }

        public ActionResult Index()
        {
            //this._servicoDeGestaoDeClientes.CadastrarNovoCliente()
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}