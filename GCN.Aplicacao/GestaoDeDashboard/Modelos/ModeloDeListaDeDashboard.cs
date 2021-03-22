using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeDashboard.Modelos
{
    public class ModeloDeListaDeDashboard
    {
        public ModeloDeListaDeDashboard()
        {
            this.Filtro = new ModeloDeFiltroDeDashboard();
            this.Lista = new List<ModeloDeDashboardDaLista>();
        }

        public ModeloDeListaDeDashboard(IEnumerable<Premiacao> lista, int totalDeRegistros, ModeloDeFiltroDeDashboard filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            this.ValorGeral = lista.Sum(a => a.ValorTotal).ToString("f");
            this.ValorArrecado = lista.Sum(a => a.ValorAdministracao).ToString("f");
        }


        public int TotalDeRegistros { get; set; }
        public string TotalApostas { get; set; }
        public string TotalSaques { get; set; }
        public string ValorGeral { get; set; }
        public string ValorArrecado { get; set; }
        public string ValorCaixa { get; set; }
        public ModeloDeFiltroDeDashboard Filtro { get; set; }
        public IList<ModeloDeDashboardDaLista> Lista { get; set; }
    }
}
