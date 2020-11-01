using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeJogos.Modelos
{
    public class ModeloDeJogosDaLista
    {
        public ModeloDeJogosDaLista(Jogo jogo)
        {
            this.Id = jogo.Id;
            this.DataHoraDoJogo = $"{ jogo.DataHoraDoJogo.ToString("dddd, dd MMMM")} às {jogo.DataHoraDoJogo.ToString("HH:mm")}";
            this.DataDoJogo = $"{ jogo.DataHoraDoJogo.ToString("dddd, dd MMMM")}" ;
            this.HoraDoJogo = jogo.DataHoraDoJogo.ToString("HH:mm");
            this.Estadio = jogo.Estadio;
            if (jogo.Time1 != null)
            {
                this.ImagemTime1 = jogo.Time1.Imagem;
                this.NomeTime1 = jogo.Time1.Nome;
                this.SiglaTime1 = jogo.Time1.Sigla;
                this.PlacarTime1 = jogo.PlacarTime1 != null ? jogo.PlacarTime1.ToString() : "";
            }
            if (jogo.Time2 != null)
            {
                this.ImagemTime2 = jogo.Time2.Imagem;
                this.NomeTime2 = jogo.Time2.Nome;
                this.SiglaTime2 = jogo.Time2.Sigla;
                this.PlacarTime2 = jogo.PlacarTime2 != null ? jogo.PlacarTime2.ToString() : "";
            }

            this.NomeEstadio = jogo.Estadio != null ? jogo.Estadio.Nome : "";
            this.NomeRodada = jogo.Rodada != null ? jogo.Rodada.Nome : "";
        }

        public int Id { get; set; }
        public string DataHoraDoJogo { get; set; }
        public string DataDoJogo { get; set; }
        public string HoraDoJogo { get; set; }
        public Estadio Estadio { get; set; }
        public string NomeEstadio { get; set; }
        public Time Time1 { get; set; }
        public string ImagemTime1 { get; set; }
        public string NomeTime1 { get; set; }
        public Time Time2 { get; set; }
        public string ImagemTime2 { get; set; }
        public string NomeTime2 { get; set; }
        public string NomeRodada { get; set; }
        public string SiglaTime1 { get; set; }
        public string SiglaTime2 { get; set; }
        public string PlacarTime1 { get; set; }
        public string PlacarTime2 { get; set; }
    }
}
