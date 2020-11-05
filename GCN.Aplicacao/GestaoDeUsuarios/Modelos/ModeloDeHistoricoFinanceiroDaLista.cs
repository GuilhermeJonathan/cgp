using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios.Modelos
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
            this.Valor = historico.Valor;
            this.Saldo = historico.Saldo;
            this.TipoDeOperacao = historico.TipoDeOperacao;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string DataDoCadastro { get; set; }
        public TipoDeOperacao TipoDeOperacao { get; set; }
    }
}
