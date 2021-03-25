using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCrimes.Modelos
{
    public class ModeloDeListaDeCrimes
    {
        public ModeloDeListaDeCrimes()
        {
            this.Filtro = new ModeloDeFiltroDeCrime();
            this.Lista = new List<ModeloDeCrimesDaLista>();
        }

        public ModeloDeListaDeCrimes(IEnumerable<Crime> lista, int totalDeRegistros, ModeloDeFiltroDeCrime filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeCrimesDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeCrime Filtro { get; set; }
        public IList<ModeloDeCrimesDaLista> Lista { get; set; }
    }
}
