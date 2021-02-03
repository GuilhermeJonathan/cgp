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
            this.HistoricosFinanceiros = new List<HistoricoFinanceiro>();
        }

        public Nome Nome { get; set; }
        public LoginUsuario Login { get; private set; }
        public Senha Senha { get; private set; }
        public bool Ativo { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public bool UsuarioNovo { get; set; }
        public decimal Saldo { get; set; }
        public ICollection<HistoricoFinanceiro> HistoricosFinanceiros { get; set; }

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

        public Usuario(Nome nome, LoginUsuario login, Senha senha) : this()
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
            this.PerfilDeUsuario = PerfilDeUsuario.Usuario;
        }

        public void AlterarDados(string nome, string email, bool ativo, PerfilDeUsuario perfilDeUsuario)
        {
            this.Nome = new Nome(nome);
            this.Login = new LoginUsuario(email);
            this.Ativo = ativo;
            this.PerfilDeUsuario = perfilDeUsuario;
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

        public void SubtrairCredito(string descricao, decimal valor, int idUsuario)
        {
            this.Saldo = Saldo - valor;
            this.HistoricosFinanceiros.Add(new HistoricoFinanceiro(descricao, valor, this.Saldo, TipoDeOperacao.Debito, idUsuario, TipoDeSolicitacaoFinanceira.Aposta));
        }

        public void AdicionarSaldo(string descricao, decimal valor, int idUsuario, TipoDeSolicitacaoFinanceira tipoDeSolicitacaoDeFinanceiro)
        {
            this.Saldo = Saldo + valor;
            this.HistoricosFinanceiros.Add(new HistoricoFinanceiro(descricao, valor, this.Saldo, TipoDeOperacao.Credito, idUsuario, tipoDeSolicitacaoDeFinanceiro));
        }
    }
}
