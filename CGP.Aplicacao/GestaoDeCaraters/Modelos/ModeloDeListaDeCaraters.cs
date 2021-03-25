using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeListaDeCaraters
    {
        public ModeloDeListaDeCaraters()
        {
            this.Filtro = new ModeloDeFiltroDeCarater();
            this.Lista = new List<ModeloDeCaratersDaLista>();
        }

        public ModeloDeListaDeCaraters(IEnumerable<Carater> lista, int totalDeRegistros, ModeloDeFiltroDeCarater filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeCaratersDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeCarater Filtro { get; set; }
        public IList<ModeloDeCaratersDaLista> Lista { get; set; }
    }
}
