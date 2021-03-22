using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTemporadas.Modelos
{
    public class ModeloDeListaDeTemporada
    {
        public ModeloDeListaDeTemporada()
        {
            this.Filtro = new ModeloDeFiltroDeTemporada();
            this.Lista = new List<ModeloDeTemporadaDaLista>();
        }

        public ModeloDeListaDeTemporada(IEnumerable<Temporada> lista, int totalDeRegistros, ModeloDeFiltroDeTemporada filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeTemporadaDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeTemporada Filtro { get; set; }
        public IList<ModeloDeTemporadaDaLista> Lista { get; set; }
    }
}
