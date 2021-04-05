using Cgp.Dominio.ObjetosDeValor;
using Cgp.Dominio.ObjetosDeValor.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Usuario : Entidade
    {
        public Usuario()
        {
            
        }

        public Nome Nome { get; set; }
        public LoginUsuario Login { get; private set; }
        public Senha Senha { get; private set; }
        public bool Ativo { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public bool UsuarioNovo { get; set; }
        public Telefone Telefone { get; set; }
        public Batalhao Batalhao { get; set; }
        public string Matricula { get; set; }
        public bool EhEstrangeiro { get; set; }
        public string TokenSenha { get; set; }

        internal void AlterarLogin(string login)
        {
            this.Login = new LoginUsuario(login);
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

        public Usuario(Nome nome, LoginUsuario login, Senha senha, Batalhao batalhao, string matricula) : this()
        {
            if (nome == null)
                throw new ExcecaoDeNegocio("Não é possível criar um usuário sem nome");

            if (login == null)
                throw new ExcecaoDeNegocio("Não é possível criar um usuário sem login");

            this.Nome = nome;
            this.Login = login;
            this.Senha = senha;
            this.Ativo = false;
            this.UsuarioNovo = true;
            this.Telefone = Telefone.Vazio;
            this.PerfilDeUsuario = PerfilDeUsuario.Usuario;
            this.Batalhao = batalhao;
            this.Matricula = matricula;
        }

        public void AlterarDados(string nome, string email, bool ativo, PerfilDeUsuario perfilDeUsuario, Batalhao batalhao, string matricula)
        {
            this.Nome = new Nome(nome);
            this.Login = new LoginUsuario(email);
            this.Ativo = ativo;
            this.PerfilDeUsuario = perfilDeUsuario;
            this.Batalhao = batalhao;
            this.Matricula = matricula;
        }

        public void AlterarMeusDados(string nome, string email, string ddd, string telefone, Batalhao batalhao, string matricula)
        {
            this.Nome = new Nome(nome);
            this.Login = new LoginUsuario(email);
            this.Batalhao = batalhao;
            this.Matricula = matricula;

            if (!String.IsNullOrEmpty(ddd) && !String.IsNullOrEmpty(telefone))
                this.Telefone = new Telefone(ddd, telefone);
            else
                this.Telefone = Telefone.Vazio;
        }

        public void AtivarUsuario()
        {
            this.UsuarioNovo = false;
            this.Ativo = true;
        }

        public void InativarUsuario()
        {
            this.Ativo = false;
        }

        public void IncluirToken(string token)
        {
            this.TokenSenha = token;
        }
    }
}
