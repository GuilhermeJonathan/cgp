using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class HistoricoFinanceiro : Entidade
    {
        public HistoricoFinanceiro()
        {

        }

        public HistoricoFinanceiro(string descricao, decimal valor, decimal saldo, TipoDeOperacao tipoDeOperacao, int idUsuario, TipoDeSolicitacaoFinanceira tipoDeSolicitacaoFinanceira)
        {
            this.Descricao = descricao;
            this.Valor = valor;
            this.Saldo = saldo;
            this.TipoDeOperacao = tipoDeOperacao;
            this.IdUsuarioCadastrou = idUsuario;
            this.TipoDeSolicitacaoFinanceira = tipoDeSolicitacaoFinanceira;
        }

        public HistoricoFinanceiro(string descricao, decimal valor, decimal saldo, TipoDeOperacao tipoDeOperacao, int idUsuario, TipoDeSolicitacaoFinanceira tipoDeSolicitacaoFinanceira, TipoDePix tipoDePix, string chavePix)
        {
            this.Descricao = descricao;
            this.Valor = valor;
            this.Saldo = saldo;
            this.TipoDeOperacao = tipoDeOperacao;
            this.IdUsuarioCadastrou = idUsuario;
            this.TipoDeSolicitacaoFinanceira = tipoDeSolicitacaoFinanceira;
            this.TipoDePix = tipoDePix;
            this.ChavePix = chavePix;
            this.RealizouPagamento = false;
        }

        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public int IdUsuarioCadastrou { get; set; }
        public TipoDeOperacao TipoDeOperacao { get; set; }
        public TipoDeSolicitacaoFinanceira TipoDeSolicitacaoFinanceira { get; set; }
        public TipoDePix TipoDePix { get; set; }
        public string ChavePix { get; set; }
        public bool RealizouPagamento { get; set; }
        public Usuario Usuario { get; set; }
        public string Comprovante { get; set; }

        public void AlterarDados(bool realizouPagamento, Usuario usuario, string comprovante)
        {
            this.RealizouPagamento = true;
            this.IdUsuarioCadastrou = usuario.Id;
            this.Comprovante = comprovante;
        }
    }
}
