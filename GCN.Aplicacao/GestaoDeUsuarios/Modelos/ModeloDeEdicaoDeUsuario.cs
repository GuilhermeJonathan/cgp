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
    public class ModeloDeEdicaoDeUsuario
    {

        public ModeloDeEdicaoDeUsuario()
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
            this.TiposDePix = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<TipoDePix>();
            this.HistoricosFinanceiros = new List<ModeloDeHistoricoFinanceiroDaLista>();
        }

        public ModeloDeEdicaoDeUsuario(Usuario usuario)
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
            this.TiposDePix = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<TipoDePix>();
            this.HistoricosFinanceiros = new List<ModeloDeHistoricoFinanceiroDaLista>();

            this.Id = usuario.Id;
            this.Nome = usuario.Nome.Valor;
            this.Email = usuario.Login.Valor;
            this.Ativo = usuario.Ativo;
            this.Credito = usuario.Saldo.ToString("f");
            this.PerfilDeUsuario = usuario.PerfilDeUsuario;
            this.TipoDePix = usuario.TipoDePix;
            this.ChavePix = usuario.ChavePix;
            this.Telefone = usuario.Telefone.Numero;
            this.Ddd = usuario.Telefone.Ddd;
            usuario.HistoricosFinanceiros.ToList().ForEach(a => this.HistoricosFinanceiros.Add(new ModeloDeHistoricoFinanceiroDaLista(a)));
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Ddd { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public string Credito { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeUsuario { get; set; }
        public IList<ModeloDeHistoricoFinanceiroDaLista> HistoricosFinanceiros { get; set; }
        public TipoDePix TipoDePix { get; set; }
        public IEnumerable<SelectListItem> TiposDePix { get; set; }
        public string ChavePix { get; set; }
    }
}
