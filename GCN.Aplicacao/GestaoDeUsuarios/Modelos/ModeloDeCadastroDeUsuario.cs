using Campeonato.Aplicacao.Util;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeCadastroDeUsuario
    {
        public ModeloDeCadastroDeUsuario()
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
        }

        public ModeloDeCadastroDeUsuario(string nome, string email, string senha)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.PerfilDeUsuario = PerfilDeUsuario.Usuario;
        }

        public string Nome { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeUsuario { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
