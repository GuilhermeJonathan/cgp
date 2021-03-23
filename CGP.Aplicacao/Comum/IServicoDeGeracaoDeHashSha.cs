using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Comum
{
    public interface IServicoDeGeracaoDeHashSha
    {
        string GerarHash(string valor);
    }
}
