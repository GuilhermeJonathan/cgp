using Campeonato.Aplicacao.GestaoDeApostas.Modelos;
using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Dominio.Entidades;
using Campeonato.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
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
            
            modelo = this._servicoDeGestaoDeApostas.BuscarResultado(modelo.Filtro.Rodada);

            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                   () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            modelo.Filtro.Rodada = id.HasValue ? id.Value : 0;

            return View(modelo);
        }
    }
}