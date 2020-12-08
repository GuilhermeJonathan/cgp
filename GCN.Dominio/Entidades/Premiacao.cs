using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Premiacao : Entidade
    {
        public Premiacao()
        {

        }

        public Rodada Rodada { get; set; }
        public Usuario PrimeiroColocado { get; set; }
        public Usuario SegundoColocado { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal PremioPrimeiro { get; set; }
        public decimal PremioSegundo { get; set; }
        public decimal ValorAcumulado { get; set; }
        public decimal ValorAdministracao { get; set; }
    }
}
