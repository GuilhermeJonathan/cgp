using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.BuscaViaDapper
{
    public interface IServicoDeBuscaViaDapper
    {
        IEnumerable<T> Buscar<T>(string query, object parametros = null);
        void Salvar(string query, object parametros = null);
    }
}
