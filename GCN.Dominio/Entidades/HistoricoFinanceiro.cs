using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
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
        }

        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public int IdUsuarioCadastrou { get; set; }
        public TipoDeOperacao TipoDeOperacao { get; set; }
        public TipoDeSolicitacaoFinanceira TipoDeSolicitacaoFinanceira { get; set; }
        public TipoDePix TipoDePix { get; set; }
        public string ChavePix { get; set; }
    }
}
