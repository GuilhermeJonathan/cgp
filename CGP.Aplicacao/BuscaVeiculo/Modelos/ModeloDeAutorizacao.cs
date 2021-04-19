using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDeAutorizacao
    {
        public ModeloDeAutorizacao()
        {
            this.grant_type = "password";
            this.username = "dtic@pm.df.gov.br";
            this.password = "pMDf@2020!!COPOM&&";
        }

        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
