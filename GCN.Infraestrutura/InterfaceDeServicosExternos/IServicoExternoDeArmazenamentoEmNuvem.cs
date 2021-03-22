using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.InterfaceDeServicosExternos
{
    public interface IServicoExternoDeArmazenamentoEmNuvem
    {
        Task<string> EnviarArquivoAsync(Stream arquivo, string caminho, string nomeDoArquivo);
        Task<Stream> RecuperarArquivo(string caminho, string nomeDoArquivo);
    }
}
