using Cgp.Aplicacao.Util;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeCadastroDeUsuario
    {
        public ModeloDeCadastroDeUsuario()
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
            this.Batalhoes = new List<SelectListItem>();
        }

        public ModeloDeCadastroDeUsuario(string nome, string email, string senha, string matricula, int batalhao)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Batalhao = batalhao;
            this.Matricula = matricula;
            this.PerfilDeUsuario = PerfilDeUsuario.Usuario;
        }

        public string Nome { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeUsuario { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Matricula { get; set; }
        public int Batalhao { get; set; }
        public IEnumerable<SelectListItem> Batalhoes { get; set; }
    }
}
