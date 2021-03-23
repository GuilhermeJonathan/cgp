using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeApostas.Modelos
{
    public class ModeloDeListaDeApostas
    {
        public ModeloDeListaDeApostas()
        {
            this.Filtro = new ModeloDeFiltroDeAposta();
            this.Lista = new List<ModeloDeApostaDaLista>();
        }

        public ModeloDeListaDeApostas(IEnumerable<Aposta> lista, int totalDeRegistros, decimal valorTotal, ModeloDeFiltroDeAposta filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            this.ValorDaRodada = valorTotal;
            var rodada = lista.FirstOrDefault() != null ? lista.FirstOrDefault().Rodada : null;
            if(rodada != null)
            {
                this.RodadaId = rodada.Id;
                this.RodadaAberta = rodada.Aberta;
                this.NomeRodada = $"Rodada{rodada.Ordem}";
                this.TemArquivo = !String.IsNullOrEmpty(rodada.CaminhoArquivo) ? true : false;
                this.CaminhoArquivo = rodada.CaminhoArquivo;
                this.RodadaFinalizada = rodada.SituacaoDaRodada == Dominio.ObjetosDeValor.SituacaoDaRodada.Finalizada;
                this.LancouPremiacao = rodada.LancouPremiacao;
            } else
            {
                this.RodadaAberta = false;
                this.NomeRodada = "";
            }

            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeApostaDaLista(a)));
        }


        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeAposta Filtro { get; set; }
        public IList<ModeloDeApostaDaLista> Lista { get; set; }
        public int RodadaId { get; set; }
        public string NomeRodada { get; set; }
        public decimal ValorDaRodada { get; set; }
        public bool RodadaAberta { get; set; }
        public bool TemArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public int QtdJogos => TotalDeRegistros;
        public string ArquivoHtml { get; set; }
        public bool RodadaFinalizada { get; set; }
        public bool LancouPremiacao { get; set; }
    }
}
