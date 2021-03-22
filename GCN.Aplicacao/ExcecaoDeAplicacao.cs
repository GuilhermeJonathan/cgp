using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao
{
    public class ExcecaoDeAplicacao : Exception
    {
        public ExcecaoDeAplicacao(string message) : base(message)
        {
        }

        public ExcecaoDeAplicacao() : base("Ocorreu um erro ao processar seu pedido. Por favor, tente mais tarde.")
        {

        }
    }
}
