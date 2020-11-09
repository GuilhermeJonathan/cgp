using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeJogos.Modelos
{
    public class ModeloDeJogosDaApostaDaLista
    {
        public ModeloDeJogosDaApostaDaLista()
        {

        }

        public ModeloDeJogosDaApostaDaLista(JogoDaAposta jogoDaAposta)
        {
            this.Id = jogoDaAposta.Id;
            this.DataHoraDoJogo = $"{ jogoDaAposta.DataHoraDoJogo.ToString("dddd, dd MMMM")} às {jogoDaAposta.DataHoraDoJogo.ToString("HH:mm")}";
            this.DataDoJogo = $"{ jogoDaAposta.DataHoraDoJogo.ToString("dddd, dd MMMM")}";
            this.HoraDoJogo = jogoDaAposta.DataHoraDoJogo.ToString("HH:mm");

            if (jogoDaAposta.Time1 != null)
            {
                this.ImagemTime1 = jogoDaAposta.Time1.Imagem;
                this.NomeTime1 = jogoDaAposta.Time1.Nome;
                this.SiglaTime1 = jogoDaAposta.Time1.Sigla;
                this.PlacarTime1 = jogoDaAposta.PlacarTime1.ToString();
            }
            if (jogoDaAposta.Time2 != null)
            {
                this.ImagemTime2 = jogoDaAposta.Time2.Imagem;
                this.NomeTime2 = jogoDaAposta.Time2.Nome;
                this.SiglaTime2 = jogoDaAposta.Time2.Sigla;
                this.PlacarTime2 = jogoDaAposta.PlacarTime2.ToString();
            }

            this.NomeEstadio = jogoDaAposta.Estadio != null ? jogoDaAposta.Estadio.Nome : "";
            this.NomeRodada = jogoDaAposta.Rodada != null ? jogoDaAposta.Rodada.Nome : "";
            RetornaClasseResultado(jogoDaAposta);
        }

        public int Id { get; set; }
        public string DataHoraDoJogo { get; set; }
        public string DataDoJogo { get; set; }
        public string HoraDoJogo { get; set; }
        public Time Time1 { get; set; }
        public string ImagemTime1 { get; set; }
        public string NomeTime1 { get; set; }
        public Time Time2 { get; set; }
        public string ImagemTime2 { get; set; }
        public string NomeTime2 { get; set; }
        public string SiglaTime1 { get; set; }
        public string SiglaTime2 { get; set; }
        public string PlacarTime1 { get; set; }
        public string PlacarTime2 { get; set; }
        public string NomeEstadio { get; set; }
        public string NomeRodada { get; set; }
        public int ResultadoDoJogoDaAposta { get; set; }
        public string CssResultadoTime1 { get; set; }
        public string CssResultadoTime2 { get; set; }


        private void RetornaClasseResultado(JogoDaAposta jogoDaAposta)
        { 
           
            switch (jogoDaAposta.ResultadoDoJogoDaAposta)
            {
                case Dominio.ObjetosDeValor.ResultadoDoJogoDaAposta.Placar:
                    this.CssResultadoTime1 = "verde";
                    this.CssResultadoTime2 = "verde";
                    break;
                case Dominio.ObjetosDeValor.ResultadoDoJogoDaAposta.Empate:
                    this.CssResultadoTime1 = "amarelo";
                    this.CssResultadoTime2 = "amarelo";
                    break;
                case Dominio.ObjetosDeValor.ResultadoDoJogoDaAposta.GanhadorTime1:
                    this.CssResultadoTime1 = "azul";
                    this.CssResultadoTime2 = "";
                    break;
                case Dominio.ObjetosDeValor.ResultadoDoJogoDaAposta.GanhadorTime2:
                    this.CssResultadoTime1 = "";
                    this.CssResultadoTime2 = "azul";
                    break;
                default:
                    break;
            }
        }
    }
}
