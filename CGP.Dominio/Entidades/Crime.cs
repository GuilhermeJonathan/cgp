using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Crime : Entidade
    {
        public Crime()
        {

        }

        public Crime(string nome, string artigo, Usuario usuario)
        {
            this.Artigo = artigo;
            this.Nome = nome;
            this.Ativo = true;
            this.UsuarioQueAlterou = usuario;
        }

        public string Artigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public Usuario UsuarioQueAlterou { get; set; }
        public ICollection<Carater> Caraters { get; set; }

        public void AlterarDados(string nome, string artigo, bool ativo, Usuario usuario)
        {
            this.Nome = nome;
            this.Artigo = artigo;
            this.Ativo = ativo;
            this.Atualizar(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            this.UsuarioQueAlterou = usuario;
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
    }
}
