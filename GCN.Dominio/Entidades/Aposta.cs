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

        }

        public Usuario Usuario { get; set; }
        public Jogo Jogo { get; set; }
        public int Pontuacao { get; set; }

    }
}
