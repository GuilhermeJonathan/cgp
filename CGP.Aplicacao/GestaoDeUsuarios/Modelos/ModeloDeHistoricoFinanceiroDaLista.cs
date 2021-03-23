using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeHistoricoFinanceiroDaLista
    {
        public ModeloDeHistoricoFinanceiroDaLista()
        {

        }

        public ModeloDeHistoricoFinanceiroDaLista(HistoricoFinanceiro historico)
        {
            this.Id = historico.Id;
            this.DataDoCadastro = historico.DataDoCadastro.ToString("g");
            this.Descricao = historico.Descricao;
            this.Usuario = historico.Usuario.Nome.Valor;

            this.Valor = historico.Valor;
            this.Saldo = historico.Saldo;
            this.ValorSaque = historico.Valor.ToString("c");
            this.ValorTexto = historico.TipoDeOperacao == TipoDeOperacao.Credito ? $"+{historico.Valor}" : historico.TipoDeOperacao == TipoDeOperacao.Debito ? $"-{historico.Valor}" : "";
            this.CssValor = historico.TipoDeOperacao == TipoDeOperacao.Credito ? $"verde" : historico.TipoDeOperacao == TipoDeOperacao.Debito ? $"vermelho" : "";
            this.TipoDeOperacao = historico.TipoDeOperacao;
            this.TipoDeSolicitacaoFinanceira = historico.TipoDeSolicitacaoFinanceira;
            this.RealizouPagamento = historico.RealizouPagamento;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Usuario { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string ValorSaque { get; set; }
        public string DataDoCadastro { get; set; }
        public string ValorTexto { get; set; }
        public string CssValor { get; set; }
        public TipoDeOperacao TipoDeOperacao { get; set; }
        public TipoDeSolicitacaoFinanceira TipoDeSolicitacaoFinanceira { get; set; }
        public bool RealizouPagamento { get; set; }
    }
}
