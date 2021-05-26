using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class UsuarioLogado
    {
        public UsuarioLogado(int id, string nome, string nomeCompleto, string email, PerfilDeUsuario perfilDeUsuario, 
            string cpf, string matricula, string lotacao)
        {
            this.Id = id;
            this.Nome = nome;
            this.NomeCompleto = nomeCompleto;
            this.Email = email;
            this.PerfilDeUsuario = perfilDeUsuario;
            this.Cpf = cpf;
            this.Matricula = matricula;
            this.Lotacao = lotacao;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public string Matricula { get; set; }
        public string Lotacao { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
    }
}
