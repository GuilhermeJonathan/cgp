using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Jogo : Entidade
    {
        public Jogo()
        {

        }

        public Jogo(DateTime dataHoraDoJogo, Estadio estadio, Time time1, Time time2, Rodada rodada)
        {
            this.DataHoraDoJogo = dataHoraDoJogo;
            this.Estadio = estadio;
            this.Time1 = time1;
            this.Time2 = time2;
            this.Rodada = rodada;
            this.SituacaoDoJogo = SituacaoDoJogo.AJogar;
        }

        public DateTime DataHoraDoJogo { get; set; }
        public Estadio Estadio { get; set; }
        public Time Time1 { get; set; }
        public Time Time2 { get; set; }
        public Rodada Rodada { get; set; }
        public int PlacarTime1 { get; set; }
        public int PlacarTime2 { get; set; }
        public SituacaoDoJogo SituacaoDoJogo { get; set; }
        public bool LancouResultado { get; set; }

        public void AlterarDadosDoJogo(DateTime dataHoraDoJogo, Estadio estadio, Time time1, Time time2, Rodada rodada)
        {
            this.DataHoraDoJogo = dataHoraDoJogo;
            this.Estadio = estadio;
            this.Time1 = time1;
            this.Time2 = time2;
            this.Rodada = rodada;

        }
    }
}
