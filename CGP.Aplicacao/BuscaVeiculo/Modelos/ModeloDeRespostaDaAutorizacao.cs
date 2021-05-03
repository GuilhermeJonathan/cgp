using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    internal class ModeloDeRespostaDaAutorizacao
    {
        public string Token { get; set; }
        public string access_token { get; set; }
        public string DataExpiration { get; set; }
    }
}
