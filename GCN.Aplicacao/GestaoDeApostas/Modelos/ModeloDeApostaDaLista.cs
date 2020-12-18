using Campeonato.Aplicacao.GestaoDeJogos.Modelos;
using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeApostas.Modelos
{
    public class ModeloDeApostaDaLista
    {
        public ModeloDeApostaDaLista(Aposta aposta)
        {
            this.Jogos = new List<ModeloDeJogosDaApostaDaLista>();

            if (aposta == null)
                return;

            this.Id = aposta.Id;
            this.NomeUsuario = aposta.Usuario.Nome.Valor;
            this.IdUsuario = aposta.Usuario.Id;
            this.NomeRodada = aposta.Rodada.Nome;
            this.IdRodada = aposta.Rodada.Id;
            this.Situacao = aposta.SituacaoDaAposta.ToString();
            this.DataDoCadastro = aposta.DataDoCadastro.ToShortDateString();
            aposta.Jogos.ToList().ForEach(a => this.Jogos.Add(new ModeloDeJogosDaApostaDaLista(a)));
            this.Pontuacao = aposta.Pontuacao;
            this.AcertoPlacar = aposta.AcertoPlacar;
            this.AcertoEmpate = aposta.AcertoEmpate;
            this.AcertoGanhador = aposta.AcertoGanhador;
            this.TipoDaAposta = aposta.TipoDeAposta.ToString();
            this.ValorDaAposta = aposta.Valor.ToString("f");
            this.RodadaAberta = aposta.Rodada.Aberta;
        }

        public ModeloDeApostaDaLista(int id, string nome,  int classificacao, int pontuacao, int acertoPlacar, int acertoEmpate, int acertoGanhador)
        {
            this.Id = id;
            this.NomeUsuario = nome;
            this.Classificacao = classificacao;
            this.Pontuacao = pontuacao;
            this.AcertoPlacar = acertoPlacar;
            this.AcertoEmpate = acertoEmpate;
            this.AcertoGanhador = acertoGanhador;
        }

        public int Classificacao { get; set; }
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public int IdRodada { get; set; }
        public string NomeRodada { get; set; }
        public string Situacao { get; set; }
        public string DataDoCadastro { get; set; }
        public IList<ModeloDeJogosDaApostaDaLista> Jogos { get; set; }

        public int Pontuacao { get; set; }
        public int AcertoPlacar { get; set; }
        public int AcertoEmpate { get; set; }
        public int AcertoGanhador { get; set; }
        public string TipoDaAposta { get; set; }
        public string ValorDaAposta { get; set; }
        public bool RodadaAberta { get; set; }
    }
}
