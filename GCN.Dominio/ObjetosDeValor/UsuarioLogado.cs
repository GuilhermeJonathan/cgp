using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.ObjetosDeValor
{
    public class UsuarioLogado
    {
        public UsuarioLogado(int id, string nome, string email, PerfilDeUsuario perfilDeUsuario)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.PerfilDeUsuario = perfilDeUsuario;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
    }
}
