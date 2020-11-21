using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Aposta : Entidade
    {
        public Aposta()
        {
            this.Jogos = new List<JogoDaAposta>();
        }

        public Aposta(Usuario usuario, Rodada rodada)
        {
            this.Jogos = new List<JogoDaAposta>();
            this.Usuario = usuario;
            this.Rodada = rodada;
            this.TipoDeAposta = TipoDeAposta.Geral;
            this.Valor = 0;
        }

        public Aposta(Usuario usuario, Rodada rodada, TipoDeAposta tipoDeAposta, decimal valorDaAposta)
        {
            this.Jogos = new List<JogoDaAposta>();
            this.Usuario = usuario;
            this.Rodada = rodada;
            this.TipoDeAposta = tipoDeAposta;
            this.Valor = valorDaAposta;
        }


        public Usuario Usuario { get; set; }
        public ICollection<JogoDaAposta> Jogos { get; set; }
        public Rodada Rodada { get; set; }
        public int Pontuacao { get; set; }
        public int AcertoPlacar { get; set; }
        public int AcertoEmpate { get; set; }
        public int AcertoGanhador { get; set; }
        public SituacaoDaAposta SituacaoDaAposta { get; set; }
        public TipoDeAposta TipoDeAposta { get; set; }
        public decimal Valor { get; set; }

    }
}
