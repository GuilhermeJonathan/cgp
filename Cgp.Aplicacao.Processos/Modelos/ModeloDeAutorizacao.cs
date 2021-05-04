using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Processos.Modelos
{
    public class ModeloDeAutorizacao
    {
        public ModeloDeAutorizacao()
        {
            this.email = ConfigurationManager.AppSettings["Cortex:usuario"];
            this.senha = ConfigurationManager.AppSettings["Cortex:senha"];
        }

        public string email { get; set; }
        public string senha { get; set; }
    }
  
}
