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
            //this.grant_type = "password";
            this.email = "ditel.sds@pm.df.gov.br";
            this.senha = "bT7TQSrNx0MX";
        }

        //public string grant_type { get; set; }
        //public string username { get; set; }
        //public string password { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}
