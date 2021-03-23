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
    public class ResultadoExclusivoController : Controller
    {
        private readonly IServicoDeGestaoDeApostas _servicoDeGestaoDeApostas;
        private readonly IServicoDeGestaoDeRodadas _servicoDeGestaoDeRodadas;

        public ResultadoExclusivoController(IServicoDeGestaoDeApostas servicoDeGestaoDeApostas, IServicoDeGestaoDeRodadas servicoDeGestaoDeRodadas)
        {
            this._servicoDeGestaoDeApostas = servicoDeGestaoDeApostas;
            this._servicoDeGestaoDeRodadas = servicoDeGestaoDeRodadas;
        }

        [Route("{id}")]
        public ActionResult Index(ModeloDeListaDeApostas modelo, int? id)
        {
            var idRodada = this._servicoDeGestaoDeRodadas.BuscarRodadaAtiva();

            if (id.HasValue)
            {
                modelo.Filtro.Rodada = id.Value;
                idRodada = id.Value;
            }
            else
                modelo.Filtro.Rodada = idRodada;

            modelo = this._servicoDeGestaoDeApostas.BuscarResultado(modelo.Filtro.Rodada, TipoDeAposta.Exclusiva);

            modelo.Filtro.Rodadas = ListaDeItensDeDominio.DaClasseComOpcaoTodos<Rodada>(nameof(Rodada.Nome), nameof(Rodada.Id),
                   () => this._servicoDeGestaoDeRodadas.RetonarTodosAsRodadasAtivas());

            modelo.Filtro.Rodada = idRodada;

            return View(modelo);
        }
    }
}