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

        public DateTime DataHoraDoJogo { get; set; }
        public Estadio Estadio { get; set; }
        public Time Time1 { get; set; }
        public Time Time2 { get; set; }
        public int PlacarTime1 { get; set; }
        public int PlacarTime2 { get; set; }
    }
}
