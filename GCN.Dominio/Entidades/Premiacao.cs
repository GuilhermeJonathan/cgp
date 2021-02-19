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

        public Premiacao(Rodada rodada, Usuario primeiroColocado, Usuario segundoColocado, decimal valorTotal, decimal premioPrimeiro, decimal premioSegundo, decimal valorAcumulado, decimal valorAdministracao, Usuario usuarioQueGerou)
        {
            this.Rodada = rodada;
            this.PrimeiroColocado = primeiroColocado;
            this.SegundoColocado = segundoColocado;
            this.ValorTotal = valorTotal;
            this.PremioPrimeiro = premioPrimeiro;
            this.PremioSegundo = premioSegundo;
            this.ValorAcumulado = valorAcumulado;
            this.ValorAdministracao = valorAdministracao;
            this.UsuarioQueGerou = usuarioQueGerou;
        }

        public Rodada Rodada { get; set; }
        public Usuario PrimeiroColocado { get; set; }
        public Usuario SegundoColocado { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal PremioPrimeiro { get; set; }
        public decimal PremioSegundo { get; set; }
        public decimal ValorAcumulado { get; set; }
        public decimal ValorAdministracao { get; set; }
        public Usuario UsuarioQueGerou { get; set; }
    }
}
