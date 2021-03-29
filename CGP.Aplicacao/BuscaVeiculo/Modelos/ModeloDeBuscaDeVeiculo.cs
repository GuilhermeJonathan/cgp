using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDeBuscaDeVeiculo
    {
        public ModeloDeBuscaDeVeiculo()
        {

        }

        public string name { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string plate { get; set; }
        public string cor { get; set; }
        public string chassi { get; set; }
        public string chassiTratado => !String.IsNullOrEmpty(this.chassi) ? this.chassi.Replace("*", "") : String.Empty;
        public string uf { get; set; }
    }
}
