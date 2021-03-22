using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeJogos.Modelos
{
    public class ModeloDeListaDeJogos
    {
        public ModeloDeListaDeJogos()
        {
            this.Filtro = new ModeloDeFiltroDeJogo();
            this.Lista = new List<ModeloDeJogosDaLista>();
        }

        public ModeloDeListaDeJogos(IEnumerable<Jogo> lista, int totalDeRegistros, ModeloDeFiltroDeJogo filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeJogosDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeJogo Filtro { get; set; }
        public IList<ModeloDeJogosDaLista> Lista { get; set; }
    }
}
