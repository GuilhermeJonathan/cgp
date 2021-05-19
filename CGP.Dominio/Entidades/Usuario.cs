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
        public LoginUsuario Login { get; set; }
        public string Email { get; private set; }
        public Senha Senha { get; private set; }
        public bool Ativo { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public bool UsuarioNovo { get; set; }
        public Telefone Telefone { get; set; }
        public Batalhao Batalhao { get; set; }
        public string Matricula { get; set; }
        public bool EhEstrangeiro { get; set; }
        public bool ValidadoGenesis { get; set; }
        public string TokenSenha { get; set; }
        public string Cpf { get; set; }
        public string Posto { get; set; }
        public string Lotacao { get; set; }
        public int CodigoLotacao { get; set; }
        public string NomeGuerra { get; set; }
        public int IdUsuarioAlterou { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public ICollection<AlertaUsuario> AlertasUsuario { get; set; }

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

        public Usuario(Nome nome, Senha senha, string matricula) : this()
        {
            if (nome == null)
                throw new ExcecaoDeNegocio("Não é possível criar um usuário sem nome");

            this.Nome = nome;
            this.Senha = senha;
            this.Telefone = Telefone.Vazio;
            this.PerfilDeUsuario = PerfilDeUsuario.Usuario;
            this.Matricula = matricula;
            this.Login = LoginUsuario.Vazio;
            this.Ativo = true;
            this.ValidadoGenesis = true;
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

        public void AlterarPerfil(PerfilDeUsuario perfilDeUsuario, int idUsuarioAlterou)
        {
            this.IdUsuarioAlterou = idUsuarioAlterou;
            this.PerfilDeUsuario = perfilDeUsuario;
            this.DataAlteracao = DateTime.Now;
        }

        public void AlterarDadosDoSgpol(Nome nome, Senha senha, string matricula, string cpf, string nomeGuerra, string posto, string lotacao, int lotacaoCodigo, string telefone) 
        {
            this.Nome = nome;
            this.Senha = senha;
            this.Matricula = matricula;
            this.Cpf = cpf;
            this.NomeGuerra = nomeGuerra;
            this.Posto = posto;
            this.Lotacao = lotacao;
            this.CodigoLotacao = lotacaoCodigo;
            this.ValidadoGenesis = true;
            this.Telefone = new Telefone(telefone);
            this.Login = LoginUsuario.Vazio;
            this.Ativo = true;
            this.UsuarioNovo = false;
            this.IdUsuarioAlterou = 0;
            this.EhEstrangeiro = false;
            this.PerfilDeUsuario = PerfilDeUsuario.Usuario;
            this.DataAlteracao = DateTime.Now;
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

        public void InserirAlertaUsuario(Alerta alerta)
        {
            if (this.AlertasUsuario == null)
                this.AlertasUsuario = new List<AlertaUsuario>();

            this.AlertasUsuario.Add(new AlertaUsuario(alerta, this));
        }
    }
}
