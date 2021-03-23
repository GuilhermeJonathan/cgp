using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeBatalhoes.Modelos
{
    public class ModeloDeListaDeBatalhoes
    {
        public ModeloDeListaDeBatalhoes()
        {
            this.Filtro = new ModeloDeFiltroDeBatalhao();
            this.Lista = new List<ModeloDeBatalhoesDaLista>();
        }

        public ModeloDeListaDeBatalhoes(IEnumerable<Batalhao> lista, int totalDeRegistros, ModeloDeFiltroDeBatalhao filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeBatalhoesDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeBatalhao Filtro { get; set; }
        public IList<ModeloDeBatalhoesDaLista> Lista { get; set; }
    }
}
