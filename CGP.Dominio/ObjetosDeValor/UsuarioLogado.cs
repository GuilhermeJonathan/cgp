using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class UsuarioLogado
    {
        public UsuarioLogado(int id, string nome, string email, PerfilDeUsuario perfilDeUsuario, string cpf)
        {
            this.Id = id;
            this.Nome = nome.Contains(' ') ? nome.Split(' ')[0].ToString() : nome;
            this.Email = email;
            this.PerfilDeUsuario = perfilDeUsuario;
            this.Cpf = cpf;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
    }
}
