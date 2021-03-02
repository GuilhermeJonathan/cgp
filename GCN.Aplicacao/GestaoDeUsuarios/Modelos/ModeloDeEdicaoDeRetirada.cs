using Campeonato.Aplicacao.Util;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeEdicaoDeRetirada
    {
        public ModeloDeEdicaoDeRetirada()
        {
            this.TiposDePix = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<TipoDePix>();
        }

        public ModeloDeEdicaoDeRetirada(HistoricoFinanceiro historicoFinanceiro)
        {
            if (historicoFinanceiro == null)
                return;

            this.Id = historicoFinanceiro.Id;
            this.IdUsuario = historicoFinanceiro.Usuario.Id;
            this.TiposDePix = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<TipoDePix>();
            this.Nome = historicoFinanceiro.Usuario.Nome.Valor;
            this.ValorSaque = historicoFinanceiro.Valor.ToString("c");
            this.ValorSaldo = historicoFinanceiro.Saldo.ToString("c");
            this.TipoDePix = historicoFinanceiro.TipoDePix;
            this.ChavePix = historicoFinanceiro.ChavePix;
            this.RealizouPagamento = !historicoFinanceiro.RealizouPagamento ? true : this.RealizouPagamento;
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string ValorSaque { get; set; }
        public string ValorSaldo { get; set; }
        public bool RealizouPagamento { get; set; }
        public TipoDePix TipoDePix { get; set; }
        public IEnumerable<SelectListItem> TiposDePix { get; set; }
        public string ChavePix { get; set; }
        public string Comprovante { get; set; }
    }
}
