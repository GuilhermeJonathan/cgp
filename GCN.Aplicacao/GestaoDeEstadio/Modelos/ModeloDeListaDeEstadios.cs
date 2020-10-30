using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeEstadio.Modelos
{
    public class ModeloDeListaDeEstadios
    {
        public ModeloDeListaDeEstadios()
        {
            this.Filtro = new ModeloDeFiltroDeEstadio();
            this.Lista = new List<ModeloDeEstadiosDaLista>();
        }

        public ModeloDeListaDeEstadios(IEnumerable<Estadio> lista, int totalDeRegistros, ModeloDeFiltroDeEstadio filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeEstadiosDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeEstadio Filtro { get; set; }
        public IList<ModeloDeEstadiosDaLista> Lista { get; set; }
    }
}
