using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class ComandoRegional : Entidade
    {
        public ComandoRegional()
        {
            this.Batalhoes = new List<Batalhao>();
        }

        public ComandoRegional(string nome, string sigla, Usuario usuario)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            this.UsuarioQueAlterou = usuario;
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Batalhao> Batalhoes { get; set; }
        public Usuario UsuarioQueAlterou { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }

        public void AlterarDados(string nome, string sigla, Usuario usuario, bool ativo)
        {
            this.Nome = nome;
            this.Sigla = sigla;
            this.Ativo = ativo;
            this.Atualizar(usuario);
        }

        public void Ativar(Usuario usuario)
        {
            this.Ativo = true;
            Atualizar(usuario);
        }

        public void Inativar(Usuario usuario)
        {
            this.Ativo = false;
            Atualizar(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            this.DataUltimaAtualizacao = DateTime.Now;
            this.UsuarioQueAlterou = usuario;
        }
    }
}
