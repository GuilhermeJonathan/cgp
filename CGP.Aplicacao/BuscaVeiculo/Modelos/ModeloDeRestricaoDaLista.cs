using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDeRestricaoDaLista
    {
        public ModeloDeRestricaoDaLista(Restricao restricao)
        {
            this.Data = restricao.dataOcorrencia.ToString();
            this.NumeroBO = restricao.numeroBO;
            this.Natureza = restricao.naturezaOcorrencia;
            this.Municipio = restricao.municipioBO;
            this.NomeDeclarante = restricao.nomeDeclarante;
            this.Historico = restricao.historico;
        }

        public string Data { get; set; }
        public string NumeroBO { get; set; }
        public string Natureza { get; set; }
        public string Municipio { get; set; }
        public string NomeDeclarante { get; set; }
        public string Historico { get; set; }
    }
}
