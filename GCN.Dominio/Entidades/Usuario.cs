using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Dominio.ObjetosDeValor.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Usuario : Entidade
    {
        public Usuario()
        {

        }

        public Usuario(Nome nome, Login login, string senha)
        {
            this.Nome = nome;
            this.Login = login;
            this.Senha = new Senha(senha);
        }

        public Nome Nome { get; set; }
        public Login Login { get; private set; }
        public Senha Senha { get; private set; }
        public bool Ativo { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }

        internal void AlterarLogin(string login)
        {
            this.Login = new Login(login);
        }

        internal void AlteraNome(string nomeFantasia)
        {
            this.Nome = new Nome(nomeFantasia);
        }

        public void AlterarSenha( Senha senha)
        {
            if (this.Senha.Valor == senha.Valor)
                throw new ExcecaoDeNegocio("Não é possível definir uma nova senha igual a atual");

            this.Senha = senha;
        }

        public Usuario(Nome nome, Login login, Senha senha) : this()
        {
            if (nome == null)
                throw new ExcecaoDeNegocio("Não é possível criar um usuário sem nome");

            if (login == null)
                throw new ExcecaoDeNegocio("Não é possível criar um usuário sem login");

            this.Nome = nome;
            this.Login = login;
            this.Senha = senha;
            this.Ativo = true;
            this.PerfilDeUsuario = PerfilDeUsuario.Administrador;
        }
    }
}
