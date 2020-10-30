using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeEdicaoDeRodada
    {
        public ModeloDeEdicaoDeRodada()
        {

        }

        public ModeloDeEdicaoDeRodada(Rodada rodada)
        {
            this.Id = rodada.Id;
            this.Nome = rodada.Nome;
            this.Temporada = rodada.Temporada;
            this.Ativo = rodada.Ativo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Temporada { get; set; }
        public bool Ativo { get; set; }
    }
}
