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
            this.Id = rodada.Id;
            this.Nome = rodada.Nome;
            this.Temporada = rodada.Temporada;
            this.Ativo = rodada.Ativo ? "Sim" : "Não";
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Temporada { get; set; }
        public IList<Jogo> Jogos { get; set; }
        public string Ativo { get; set; }
    }
}
