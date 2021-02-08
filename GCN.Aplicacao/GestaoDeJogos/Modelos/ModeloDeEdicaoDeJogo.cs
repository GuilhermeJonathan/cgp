using Campeonato.Aplicacao.Util;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeJogos.Modelos
{
    public class ModeloDeEdicaoDeJogo
    {
        public ModeloDeEdicaoDeJogo()
        {
            this.Times1 = new List<SelectListItem>();
            this.Times2 = new List<SelectListItem>();
            this.Estadios = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
            this.SituacoesDoJogo = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<SituacaoDoJogo>();
        }

        public ModeloDeEdicaoDeJogo(Jogo jogo)
        {
            this.SituacoesDoJogo = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<SituacaoDoJogo>();

            if (jogo == null)
                return;

            this.Id = jogo.Id;
            this.DataDoJogo = jogo.DataHoraDoJogo.ToShortDateString();
            this.HoraDoJogo = jogo.DataHoraDoJogo.ToString("HH:mm");
            this.Time1 = jogo.Time1 != null ? jogo.Time1.Id : 0;
            this.Time2 = jogo.Time2 != null ? jogo.Time2.Id : 0;
            this.Estadio = jogo.Estadio != null ? jogo.Estadio.Id : 0;
            this.Rodada = jogo.Rodada != null ? jogo.Rodada.Id : 0;
            this.SituacaoDoJogo = jogo.SituacaoDoJogo;
        }

        public int Id { get; set; }
        public string DataDoJogo { get; set; }
        public string HoraDoJogo { get; set; }
        public int Time1 { get; set; }
        public IEnumerable<SelectListItem> Times1 { get; set; }
        public int Time2 { get; set; }
        public IEnumerable<SelectListItem> Times2 { get; set; }
        public int Estadio { get; set; }
        public IEnumerable<SelectListItem> Estadios { get; set; }
        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
        public SituacaoDoJogo SituacaoDoJogo { get; set; }
        public IEnumerable<SelectListItem> SituacoesDoJogo { get; set; }

    }
}
