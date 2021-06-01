using Cgp.Aplicacao.GestaoDeCaraters;
using Cgp.Aplicacao.GestaoDeCidades;
using Cgp.Aplicacao.GestaoDeCrimes;
using Cgp.Aplicacao.GestaoDeHistoricoDePassagens;
using Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
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
    public class HistoricoPassagemController : Controller
    {
        private readonly IServicoDeGestaoDeCaraters _servicoDeGestaoDeCaraters;
        private readonly IServicoDeGestaoDeCidades _servicoDeGestaoDeCidades;
        private readonly IServicoDeGestaoDeCrimes _servicoDeGestaoDeCrimes;
        private readonly IServicoDeHistoricoDePassagens _servicoDeHistoricoDePassagens;

        public HistoricoPassagemController(IServicoDeGestaoDeCaraters servicoDeGestaoDeCaraters, IServicoDeGestaoDeCidades servicoDeGestaoDeCidades,
            IServicoDeGestaoDeCrimes servicoDeGestaoDeCrimes, IServicoDeHistoricoDePassagens servicoDeHistoricoDePassagens)
        {
            this._servicoDeGestaoDeCaraters = servicoDeGestaoDeCaraters;
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
            this._servicoDeGestaoDeCrimes = servicoDeGestaoDeCrimes;
            this._servicoDeHistoricoDePassagens = servicoDeHistoricoDePassagens;
        }

        public ActionResult Index(ModeloDeListaDeHistoricoDePassagens modelo)
        {
            modelo = this._servicoDeHistoricoDePassagens.RetornarHistociosDePassagensPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.Cidades = ListaDeItensDeDominio.DaClasseSemOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            modelo.Filtro.Crimes = ListaDeItensDeDominio.DaClasseSemOpcaoPadrao<Crime>(nameof(Crime.Nome), nameof(Crime.Id),
               () => this._servicoDeGestaoDeCrimes.RetonarTodosOsCrimesAtivos());

            if (modelo.Filtro.CidadesSelecionadas != null)
            {
                foreach (var cidade in modelo.Filtro.CidadesSelecionadas)
                    modelo.Filtro.Cidades.FirstOrDefault(a => a.Value == cidade.ToString()).Selected = true;
            }

            if (modelo.Filtro.CrimesSelecionados != null)
            {
                foreach (var crime in modelo.Filtro.CrimesSelecionados)
                    modelo.Filtro.Crimes.FirstOrDefault(a => a.Value == crime.ToString()).Selected = true;
            }

            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }
    }
}