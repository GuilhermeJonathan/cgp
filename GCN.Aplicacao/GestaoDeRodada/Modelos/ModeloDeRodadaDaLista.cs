using Campeonato.Aplicacao.GestaoDeJogos.Modelos;
using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeRodadasDaLista
    {
        public ModeloDeRodadasDaLista(Rodada rodada)
        {
            this.Jogos = new List<ModeloDeJogosDaLista>();

            this.Id = rodada.Id;
            this.Nome = rodada.Nome;
            this.Temporada = rodada.Temporada;
            this.Ativo = rodada.Ativo ? "Sim" : "Não";
            rodada.Jogos.ToList().ForEach(a => this.Jogos.Add(new ModeloDeJogosDaLista(a)));
            this.QuantidadeDeJogos = rodada.Jogos.Count;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Temporada { get; set; }
        public IList<ModeloDeJogosDaLista> Jogos { get; set; }
        public string Ativo { get; set; }
        public int QuantidadeDeJogos { get; set; }
    }
}
