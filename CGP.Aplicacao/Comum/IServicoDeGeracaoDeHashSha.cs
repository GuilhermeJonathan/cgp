using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Comum
{
    public interface IServicoDeGeracaoDeHashSha
    {
        string GerarHash(string valor);
        string GerarParaStream(Stream s);

        string EncodeToBase64(string texto);
        string DecodeFrom64(string dados);
    }
}
