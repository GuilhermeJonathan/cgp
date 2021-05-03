using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.ComunicacaoViaHttp
{
    public interface IServicoDeComunicacaoViaHttp
    {
        Task<T> Get<T>(Uri url, IDictionary<string, string> parametros = null, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(),
            IDictionary<string, string> cabecalho = null);
        Task<TRetorno> PostJson<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(),
            IDictionary<string, string> cabecalho = null);

        Task<TRetorno> PostJsonSemToken<T, TRetorno>(Uri url, T dados, IDictionary<string, string> cabecalho = null);

        Task<TRetorno> PostFormUrlEncoded<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(),
            IDictionary<string, string> cabecalho = null);

        Task<TRetorno> PostJsonGeral<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao, IDictionary<string, string> cabecalho = null);

        Task<TRetorno> PutJson<T, TRetorno>(Uri uri, T dados, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(),
            IDictionary<string, string> cabecalho = null);
    }
}
