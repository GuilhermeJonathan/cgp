using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Batalhao : Entidade
    {
        public Batalhao()
        {
            this.Usuarios = new List<Usuario>();
        }

        public Batalhao(string nome, string sigla, string cidade, ComandoRegional comandoRegional, Usuario usuario)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            this.Cidade = cidade;
            this.ComandoRegional = comandoRegional;
            this.UsuarioQueAlterou = usuario;
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }
        public ComandoRegional ComandoRegional { get; set; }
        public Usuario UsuarioQueAlterou { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }

        public void AlterarDados(string nome, string sigla, ComandoRegional comando, Usuario usuario, bool ativo)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            this.ComandoRegional = comando;
            this.Ativo = ativo;
            this.Atualizar(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            this.DataUltimaAtualizacao = DateTime.Now;
            this.UsuarioQueAlterou = usuario;
        }
    }
}
