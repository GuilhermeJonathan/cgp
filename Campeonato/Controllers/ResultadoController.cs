using Cgp.Aplicacao.GestaoDeApostas.Modelos;
using Cgp.Aplicacao.GestaoDeRodada;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    [Authorize]
    [TratarErros]
    public class ResultadoController : Controller
    {
        private readonly IServicoDeGestaoDeApostas _servicoDeGestaoDeApostas;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;
        
        public ResultadoController(IServicoDeGestaoDeApostas servicoDeGestaoDeApostas, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas)
        {
            this._servicoDeGestaoDeApostas = servicoDeGestaoDeApostas;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
        }

        [Route("{id}")]
        public ActionResult Index(ModeloDeListaDeApostas modelo, int? id)
        {
            if (id.HasValue)
                modelo.Filtro.Rodada = id.Value;
            
            modelo = this._servicoDeGestaoDeApostas.BuscarResultado(modelo.Filtro.Rodada, TipoDeAposta.Geral);

            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                   () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            modelo.Filtro.Rodada = id.HasValue ? id.Value : 0;

            return View(modelo);
        }
    }
}