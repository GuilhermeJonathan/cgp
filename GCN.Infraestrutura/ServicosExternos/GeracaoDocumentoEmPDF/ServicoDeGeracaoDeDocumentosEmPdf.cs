using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.GeracaoDocumentoEmPDF
{
    public class ServicoDeGeracaoDeDocumentosEmPdf : IServicoDeGeracaoDeDocumentosEmPdf
    {
        public byte[] CriarPdf(string documento)
        {
            return new HtmlToPdfConverter().GeneratePdf(documento);
        }
    }
}
