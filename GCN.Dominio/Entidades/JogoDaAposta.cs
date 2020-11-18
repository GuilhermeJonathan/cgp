using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class JogoDaAposta : Entidade
    {
        public JogoDaAposta()
        {

        }

        public JogoDaAposta(DateTime dataHoraDoJogo, Time time1, Time time2, Rodada rodada, Estadio estadio, int placarTime1, int placarTime2)
        {
            this.DataHoraDoJogo = dataHoraDoJogo;
            this.Time1 = time1;
            this.Time2 = time2;
            this.Rodada = rodada;
            this.Estadio = estadio;
            this.PlacarTime1 = placarTime1;
            this.PlacarTime2 = placarTime2;
            this.SituacaoDoJogo = SituacaoDoJogo.AJogar;
        }

        public DateTime DataHoraDoJogo { get; set; }
        public Time Time1 { get; set; }
        public Time Time2 { get; set; }
        public Rodada Rodada { get; set; }
        public Estadio Estadio { get; set; }
        public int PlacarTime1 { get; set; }
        public int PlacarTime2 { get; set; }
        public SituacaoDoJogo SituacaoDoJogo { get; set; }
        public ResultadoDoJogoDaAposta ResultadoDoJogoDaAposta { get; set; }

        public void AlterarDadosDoJogo(DateTime dataHoraDoJogo, Estadio estadio)
        {
            this.DataHoraDoJogo = dataHoraDoJogo;
            this.Estadio = estadio;
        }
    }
}
