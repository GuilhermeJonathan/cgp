using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDePremiacoes.Modelos
{
    public class ModeloDePremiacaoDaLista
    {
        public ModeloDePremiacaoDaLista(Premiacao premiacao)
        {
            if (premiacao == null)
                return;

            this.Id = premiacao.Id;
            this.NomeRodada = premiacao.Rodada.Nome;
            this.PrimeiroColocado = premiacao.PrimeiroColocado.Nome.Valor;
            this.SegundoColocado = premiacao.SegundoColocado.Nome.Valor;
            this.DataDaPremiacao = premiacao.DataDoCadastro.ToShortDateString();
            this.ValorDaPremiacao = premiacao.ValorTotal.ToString("f");
            this.ValorDaAdministracao = premiacao.ValorAdministracao.ToString("f");
        }

        public int Id { get; set; }
        public string NomeRodada { get; set; }
        public string PrimeiroColocado { get; set; }
        public string SegundoColocado { get; set; }
        public string DataDaPremiacao { get; set; }
        public string ValorDaPremiacao { get; set; }
        public string ValorDaAdministracao { get; set; }

    }
}
