using Cgp.Aplicacao.BuscaVeiculo;
using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.GestaoDeCaraters;
using Cgp.CustomExtensions;
using Cgp.Filter;
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
    public class BuscaController : Controller
    {
        private readonly IServicoDeGestaoDeCaraters _servicoDeGestaoDeCaraters;
        private readonly IServicoDeBuscaDeVeiculo _servicoDeGestaoDeVeiculos;

        public BuscaController(IServicoDeGestaoDeCaraters servicoDeGestaoDeCaraters, IServicoDeBuscaDeVeiculo servicoDeGestaoDeVeiculos)
        {
         
            this._servicoDeGestaoDeVeiculos = servicoDeGestaoDeVeiculos;
            this._servicoDeGestaoDeCaraters = servicoDeGestaoDeCaraters;
        }

        public async Task<ActionResult> Index(ModeloDeListaDeBuscas modelo)
        {
            if (!String.IsNullOrEmpty(modelo.Filtro.Placa) || !String.IsNullOrEmpty(modelo.Filtro.Cpf))
                modelo = await this._servicoDeGestaoDeVeiculos.BuscarPlacasPorFiltro(modelo.Filtro, User.Logado());

            return View(modelo);
        }

        [HttpGet]
        public async Task<ActionResult> Detalhar(string busca)
        {
            var modelo = new ModeloDeListaDeBuscas();
            if (String.IsNullOrEmpty(busca))
                VeiculoNaoEncontrado();

            modelo.Filtro.Placa = busca;
            var retorno = await this._servicoDeGestaoDeVeiculos.DetalharVeiculo(modelo.Filtro, User.Logado());
            return View(retorno);
        }

        private ActionResult VeiculoNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("Veículo não encontrado");
            return RedirectToAction(nameof(Index));
        }
    }
}