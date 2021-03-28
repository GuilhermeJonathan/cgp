using Cgp.Aplicacao.GestaoDeCaraters;
using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Aplicacao.GestaoDeCidades;
using Cgp.Aplicacao.GestaoDeCrimes;
using Cgp.Aplicacao.GestaoDeDashboard;
using Cgp.Aplicacao.GestaoDeDashboard.Modelos;
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

        public HomeController(IServicoDeGestaoDeDashboard servicoDeGestaoDeDashboard, IServicoDeGestaoDeCaraters servicoDeGestaoDeCaraters, IServicoDeGestaoDeCidades servicoDeGestaoDeCidades,
            IServicoDeGestaoDeCrimes servicoDeGestaoDeCrimes)
        {
            this._servicoDeGestaoDeDashboard = servicoDeGestaoDeDashboard;
            this._servicoDeGestaoDeCaraters = servicoDeGestaoDeCaraters;
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
            this._servicoDeGestaoDeCrimes = servicoDeGestaoDeCrimes;
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.Autenticado())
            {
                ViewBag.Usuario = User.Logado().Nome;
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

        public ActionResult Contato()
        {
            return View();
        }
    }
}