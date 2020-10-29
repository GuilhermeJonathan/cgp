using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Socio : Entidade
    {
        public Socio()
        {

        }

        public Socio(string nome, string documento, string email, Senha senha, string telefone, string celular, PerfilDeSocio perfilDeSocio, Endereco endereco)
        {
            this.Nome = nome;
            this.Documento = new Documento(documento);
            this.Telefone = new Telefone(telefone);
            this.Celular = new Telefone(celular);
            this.PerfilDeSocio = perfilDeSocio;
            this.Ativo = true;
            this.Endereco = endereco;

            this.Usuario = new Usuario(new Nome(this.Nome), new Login(email), senha);
        }

        public string Nome { get; set; }
        public Documento Documento { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }
        public Telefone Celular { get; set; }
        public Usuario Usuario { get; private set; }
        public PerfilDeSocio PerfilDeSocio { get; set; }
        public bool Ativo { get; set; }
    }
}
