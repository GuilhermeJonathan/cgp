using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDePremiacoes.Modelos
{
    public class ModeloDeListaDePremiacoes
    {
        public ModeloDeListaDePremiacoes()
        {
            this.Filtro = new ModeloDeFiltroDePremiacao();
            this.Lista = new List<ModeloDePremiacaoDaLista>();
        }

        public ModeloDeListaDePremiacoes(IEnumerable<Premiacao> lista, int totalDeRegistros, ModeloDeFiltroDePremiacao filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDePremiacaoDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDePremiacao Filtro { get; set; }
        public IList<ModeloDePremiacaoDaLista> Lista { get; set; }
    }
}
