using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Temporada : Entidade
    {
        public Temporada()
        {

        }

        public Temporada(string nome, string ano, string pais, Usuario usuario)
        {
            this.Nome = nome;
            this.Ano = ano;
            this.Pais = pais;
            this.UsuarioQueAlterou = usuario;
            this.Ativo = true;
            this.DataUltimaAtualizacao = DateTime.Now;
        }

        public string Nome { get; set; }
        public string Ano { get; set; }
        public string Pais { get; set; }
        public Usuario UsuarioQueAlterou { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public IList<Rodada> Rodadas { get; set; }
        public bool Aberta { get; set; }

        public void AlterarDados(string nome, string ano, string pais, bool ativo, bool aberta, Usuario usuario)
        {
            this.Nome = nome;
            this.Ano = ano;
            this.Pais = pais;
            this.Ativo = ativo;
            this.Aberta = aberta;
            this.Atualizar(usuario);
        }

        public void AbrirTemporada(Usuario usuario)
        {
            this.Aberta = true;
            this.Atualizar(usuario);
        }

        public void FecharTemporada(Usuario usuario)
        {
            this.Aberta = false;
            this.Atualizar(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            this.DataUltimaAtualizacao = DateTime.Now;
            this.UsuarioQueAlterou = usuario;
        }
    }
}
