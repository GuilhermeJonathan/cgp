using Campeonato.Aplicacao.GestaoDeJogos.Modelos;
using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeEdicaoDeRodada
    {
        public ModeloDeEdicaoDeRodada()
        {
            this.Jogos = new List<ModeloDeJogosDaLista>();
            this.Temporadas = new List<SelectListItem>();
        }

        public ModeloDeEdicaoDeRodada(Rodada rodada)
        {
            this.Jogos = new List<ModeloDeJogosDaLista>();
            this.Id = rodada.Id;
            this.Nome = rodada.Nome;
            this.NomeTemporada = rodada.Temporada != null ? rodada.Temporada.Nome : String.Empty;
            this.Temporada = rodada.Temporada.Id;
            this.Ativo = rodada.Ativo;
            rodada.Jogos.ToList().ForEach(a => this.Jogos.Add(new ModeloDeJogosDaLista(a)));
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeTemporada { get; set; }
        public int Temporada { get; set; }
        public IEnumerable<SelectListItem> Temporadas { get; set; }
        public bool Ativo { get; set; }
        public IList<ModeloDeJogosDaLista> Jogos { get; set; }
        public decimal ValorDasApostas { get; set; }
        public int PrimeiroColocado { get; set; }
        public int SegundoColocado { get; set; }
    }
}
