using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeListaDeHistoricosFinanceiros
    {
        public ModeloDeListaDeHistoricosFinanceiros()
        {
            this.Filtro = new ModeloDeFiltroDeHistoricoFinanceiro();
            this.Lista = new List<ModeloDeHistoricoFinanceiroDaLista>();
        }

        public ModeloDeListaDeHistoricosFinanceiros(IEnumerable<HistoricoFinanceiro> lista, int totalDeRegistros, ModeloDeFiltroDeHistoricoFinanceiro filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeHistoricoFinanceiroDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeHistoricoFinanceiro Filtro { get; set; }
        public IList<ModeloDeHistoricoFinanceiroDaLista> Lista { get; set; }
    }
}
