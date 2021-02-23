using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDePremiacoes.Modelos
{
    public class ModeloDeEdicaoDePremiacao
    {
        public ModeloDeEdicaoDePremiacao()
        {
                
        }

        public ModeloDeEdicaoDePremiacao(Premiacao premiacao)
        {
            if (premiacao == null)
                return;

            this.Id = premiacao.Id;
            this.NomeRodada = premiacao.Rodada.Nome;
            this.NomePrimeiroColocado = premiacao.PrimeiroColocado.Nome.Valor;
            this.PremiacaoPrimeiro = premiacao.PremioPrimeiro.ToString("f");
            this.NomeSegundoColocado = premiacao.SegundoColocado.Nome.Valor;
            this.PremiacaoSegundo = premiacao.PremioSegundo.ToString("f");
            this.Acumulado = premiacao.ValorAcumulado.ToString("f");
            this.Administracao = premiacao.ValorAdministracao.ToString("f");
            this.Total = premiacao.ValorTotal.ToString("f");
            this.UsuarioQueFechou = premiacao.UsuarioQueGerou.Nome.Valor;
            this.DataHoraFechou = $"{premiacao.DataDoCadastro.ToLongDateString()} às {premiacao.DataDoCadastro.ToShortTimeString()}";
        }

        public int Id { get; set; }
        public string NomeRodada { get; set; }
        public string NomePrimeiroColocado { get; set; }
        public string PremiacaoPrimeiro { get; set; }
        public string NomeSegundoColocado { get; set; }
        public string PremiacaoSegundo { get; set; }
        public string Acumulado { get; set; }
        public string Administracao { get; set; }
        public string Total { get; set; }
        public string UsuarioQueFechou { get; set; }
        public string DataHoraFechou { get; set; }
    }
}
