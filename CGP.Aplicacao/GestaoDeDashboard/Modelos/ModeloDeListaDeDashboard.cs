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

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeDashboard Filtro { get; set; }
        public IList<ModeloDeDashboardDaLista> Lista { get; set; }
    }
}
