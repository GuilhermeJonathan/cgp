using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais.Modelos
{
    public class ModeloDeListaDeComandosRegionais
    {
        public ModeloDeListaDeComandosRegionais()
        {
            this.Filtro = new ModeloDeFiltroDeComandoRegional();
            this.Lista = new List<ModeloDeComandosRegionaisDaLista>();
        }

        public ModeloDeListaDeComandosRegionais(IEnumerable<ComandoRegional> lista, int totalDeRegistros, ModeloDeFiltroDeComandoRegional filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeComandosRegionaisDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeComandoRegional Filtro { get; set; }
        public IList<ModeloDeComandosRegionaisDaLista> Lista { get; set; }
    }
}
