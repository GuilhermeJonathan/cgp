using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeApostas.Modelos
{
    public class ModeloDeListaDeApostas
    {
        public ModeloDeListaDeApostas()
        {
            this.Filtro = new ModeloDeFiltroDeAposta();
            this.Lista = new List<ModeloDeApostaDaLista>();
        }

        public ModeloDeListaDeApostas(IEnumerable<Aposta> lista, int totalDeRegistros, decimal valorTotal, ModeloDeFiltroDeAposta filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            this.ValorDaRodada = valorTotal;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeApostaDaLista(a)));
        }


        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeAposta Filtro { get; set; }
        public IList<ModeloDeApostaDaLista> Lista { get; set; }
        public decimal ValorDaRodada { get; set; }
        public int QtdJogos => TotalDeRegistros;
    }
}
