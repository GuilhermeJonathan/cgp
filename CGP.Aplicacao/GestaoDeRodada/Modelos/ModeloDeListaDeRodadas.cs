using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeListaDeRodadas
    {
        public ModeloDeListaDeRodadas()
        {
            this.Filtro = new ModeloDeFiltroDeRodada();
            this.Lista = new List<ModeloDeRodadasDaLista>();
        }

        public ModeloDeListaDeRodadas(IEnumerable<Rodada> lista, int totalDeRegistros, ModeloDeFiltroDeRodada filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeRodadasDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeRodada Filtro { get; set; }
        public IList<ModeloDeRodadasDaLista> Lista { get; set; }
    }
}

