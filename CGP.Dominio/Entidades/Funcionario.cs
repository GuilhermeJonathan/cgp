using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio
{
    public class Funcionario : Entidade
    {
        public Funcionario()
        {

        }

        public Funcionario(string nome, string documento, string email, Senha senha, string telefone, string celular, PerfilDeFuncionario perfilDeFuncionario, Endereco endereco)
        {
            this.Nome = nome;
            this.Documento = new Documento(documento);
            this.Telefone = new Telefone(telefone);
            this.Celular = new Telefone(celular);
            this.PerfilDeFuncionario = perfilDeFuncionario;
            this.Ativo = true;
            this.Endereco = endereco;

            this.Usuario = new Usuario(new Nome(this.Nome), new LoginUsuario(email), senha, null);
        }

        public string Nome { get; set; }
        public Documento Documento { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }
        public Telefone Celular { get; set; }
        public Usuario Usuario { get; private set; }
        public PerfilDeFuncionario PerfilDeFuncionario { get; set; }
        public bool Ativo { get; set; }
    }
}
