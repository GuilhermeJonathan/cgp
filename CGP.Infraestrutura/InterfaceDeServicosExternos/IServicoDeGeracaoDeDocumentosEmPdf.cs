using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.InterfaceDeServicosExternos
{
    public interface IServicoDeGeracaoDeDocumentosEmPdf
    {
        byte[] CriarPdf(string documento);
    }
}
