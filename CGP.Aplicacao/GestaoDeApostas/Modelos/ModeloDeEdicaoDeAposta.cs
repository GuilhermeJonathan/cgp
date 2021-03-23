using Cgp.Aplicacao.GestaoDeJogos.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeApostas.Modelos
{
    public class ModeloDeEdicaoDeAposta
    {
        public ModeloDeEdicaoDeAposta()
        {
            this.Usuarios = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
            this.Jogos = new List<ModeloDeJogosDaApostaDaLista>();
        }

        public ModeloDeEdicaoDeAposta(Aposta aposta)
        {
            var situacoesRodadaAberta = new SituacaoDaRodada[] { SituacaoDaRodada.Atual, SituacaoDaRodada.Futura };
            this.Jogos = new List<ModeloDeJogosDaApostaDaLista>();

            if (aposta == null)
                return;

            this.Id = aposta.Id;
            this.IdRodada = aposta.Rodada.Id;
            this.NomeRodada = aposta.Rodada.Nome;
            this.RodadaFechada = aposta.Rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada ? true : aposta.Rodada.DataPrimeiroJogo.Value.AddMinutes(-VariaveisDeAmbiente.Pegar<int>("TempoParaFechamentoDeRodada")) < DateTime.Now ? true : false;
            this.RodadaPodeAlterar = situacoesRodadaAberta.Contains(aposta.Rodada.SituacaoDaRodada) ? true : false;

            this.Usuario = aposta.Usuario.Id;
            this.NomeUsuario = aposta.Usuario.Nome.Valor;
            this.Rodada = aposta.Rodada.Id;
            aposta.Jogos.OrderBy(a => a.DataHoraDoJogo).ToList().ForEach(a => this.Jogos.Add(new ModeloDeJogosDaApostaDaLista(a)));

            this.EhRodadaExclusiva = aposta.TipoDeAposta == TipoDeAposta.Exclusiva ? true : false;
            this.NomeTipoDeAposta = aposta.TipoDeAposta.ToString();
            this.RodadaFinalizada = aposta.Rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada ? true : false;
            this.Pontuacao = aposta.Pontuacao;
            this.ValorDaAposta = VariaveisDeAmbiente.Pegar<decimal>("ValorDaAposta").ToString("f");
        }

        public int Id { get; set; }
        public int IdRodada { get; set; }
        public int Usuario { get; set; }
        public string NomeUsuario { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
        public string NomeRodada { get; set; }
        public IList<ModeloDeJogosDaApostaDaLista> Jogos { get; set; }
        public bool RodadaFechada { get; set; }
        public bool RodadaPodeAlterar { get; set; }
        public bool EhRodadaExclusiva { get; set; }
        public bool TemApostaExclusiva { get; set; }
        public int IdApostaExclusiva { get; set; }
        public string NomeTipoDeAposta { get; set; }
        public bool RodadaFinalizada { get; set; }
        public int Pontuacao { get; set; }
        public string ValorDaAposta { get; set; }

    }
}
