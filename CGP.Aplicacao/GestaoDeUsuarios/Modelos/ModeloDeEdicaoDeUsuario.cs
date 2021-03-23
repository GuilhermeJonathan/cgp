using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeEdicaoDeUsuario
    {

        public ModeloDeEdicaoDeUsuario()
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
            this.HistoricosFinanceiros = new List<ModeloDeHistoricoFinanceiroDaLista>();
            this.Batalhoes = new List<SelectListItem>();
        }

        public ModeloDeEdicaoDeUsuario(Usuario usuario)
        {
            if (usuario == null)
                return;

            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
            this.HistoricosFinanceiros = new List<ModeloDeHistoricoFinanceiroDaLista>();
            this.Batalhoes = new List<SelectListItem>();

            this.Id = usuario.Id;
            this.Nome = usuario.Nome.Valor;
            this.Email = usuario.Login.Valor;
            this.Ativo = usuario.Ativo;
            this.PerfilDeUsuario = usuario.PerfilDeUsuario;
            this.Telefone = usuario.Telefone.Numero;
            this.Ddd = usuario.Telefone.Ddd;
            usuario.HistoricosFinanceiros.ToList().ForEach(a => this.HistoricosFinanceiros.Add(new ModeloDeHistoricoFinanceiroDaLista(a)));
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string RepetirSenha { get; set; }
        public string Ddd { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeUsuario { get; set; }
        public IList<ModeloDeHistoricoFinanceiroDaLista> HistoricosFinanceiros { get; set; }
        public int Batalhao { get; set; }
        public IEnumerable<SelectListItem> Batalhoes { get; set; }
    }
}
