using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Rodada : Entidade
    {
        public Rodada()
        {
            this.Jogos = new List<Jogo>();
        }

        public Rodada(string nome, string temporada)
        {
            this.Nome = nome;
            this.Temporada = temporada;
            this.Ativo = true;
            this.Aberta = false;
        }

        public string Nome { get; set; }
        public IList<Jogo> Jogos { get; set; }
        public string Temporada { get; set; }
        public int Ordem { get; set; }
        public SituacaoDaRodada SituacaoDaRodada { get; set; }
        public Usuario UsuarioQueAlterou { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public bool Aberta { get; set; }
        public DateTime DataPrimeiroJogo { get; set; }

        public void AlterarRodada(string nome, string temporada, bool ativo)
        {
            this.Nome = nome;
            this.Temporada = temporada;
            this.Ativo = ativo;
        }

        public void IncluirJogoNaRodada(Jogo jogo)
        {
            this.Jogos.Add(jogo);
        }

        public void IncluirAlteracao(DateTime data, Usuario usuario)
        {
            this.DataUltimaAtualizacao = data;
            this.UsuarioQueAlterou = usuario;
        }

        public DateTime RetornaDataPrimeiroJogo()
        {
            return this.Jogos.OrderBy(a => a.DataHoraDoJogo).FirstOrDefault().DataHoraDoJogo;
        }

        public void FecharRodada(Usuario usuario)
        {
            this.DataUltimaAtualizacao = DateTime.Now;
            this.Aberta = true;
        }

        public void AbrirRodada(Usuario usuario)
        {
            this.DataUltimaAtualizacao = DateTime.Now;
            this.Aberta = false;
        }

    }
}
