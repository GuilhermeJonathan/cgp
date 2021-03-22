using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeTimes.Modelos
{
    public class ModeloDeListaDeTimes
    {
        public ModeloDeListaDeTimes()
        {
            this.Filtro = new ModeloDeFiltroDeTime();
            this.Lista = new List<ModeloDeTimesDaLista>();
        }

        public ModeloDeListaDeTimes(IEnumerable<Time> lista, int totalDeRegistros, ModeloDeFiltroDeTime filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeTimesDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeTime Filtro { get; set; }
        public IList<ModeloDeTimesDaLista> Lista { get; set; }
    }
}
