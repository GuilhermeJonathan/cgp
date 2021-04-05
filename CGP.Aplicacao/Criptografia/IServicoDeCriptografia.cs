using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Criptografia
{
    public interface IServicoDeCriptografia
    {
        string Encriptar(string valor);
        string Decriptar(string valor);
    }
}
