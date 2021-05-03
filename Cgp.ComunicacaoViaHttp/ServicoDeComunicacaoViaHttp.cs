using Cgp.Aplicacao.ComunicacaoViaHttp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cgp.ComunicacaoViaHttp
{
    public class ServicoDeComunicacaoViaHttp : IServicoDeComunicacaoViaHttp
    {
        public const string ApplicationJson = "application/json";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";
        private HttpClient _clienteHttp;

        public ServicoDeComunicacaoViaHttp()
        {
            this._clienteHttp = new HttpClient();
        }

        public async Task<T> Get<T>(Uri url, IDictionary<string, string> parametros = null, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(),
            IDictionary<string, string> cabecalho = null)
        {
            var urlCompleta = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(urlCompleta.Query);

            if (parametros != null)
            {
                foreach (var parametro in parametros)
                {
                    query.Add(parametro.Key, parametro.Value);
                }
            }

            this.MontarCabecalho(cabecalho);
            this.DefinirTokenDeAutorizacao(tokenDeAutorizacao);

            urlCompleta.Query = query.ToString();

            try
            {
                using (var resposta = await this._clienteHttp.GetAsync(urlCompleta.Uri, HttpCompletionOption.ResponseContentRead))
                {
                    resposta.EnsureSuccessStatusCode();

                    var conteudoDaResposta = await resposta.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(conteudoDaResposta);
                };
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Servidor indisponível");
            }
        }

        public async Task<TRetorno> PostJson<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(), IDictionary<string, string> cabecalho = null)
        {
            var resposta = await this.Post<dynamic>(url, dados, tokenDeAutorizacao, cabecalho, this.MontarConteudoJson);
            return JsonConvert.DeserializeObject<TRetorno>(resposta);
        }

        public async Task<TRetorno> PostJsonGeral<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao, IDictionary<string, string> cabecalho = null)
        {
            var resposta = await this.PostGeral<dynamic>(url, dados, tokenDeAutorizacao, cabecalho);
            return JsonConvert.DeserializeObject<TRetorno>(resposta);
        }

        public async Task<TRetorno> PostJsonSemToken<T, TRetorno>(Uri url, T dados, IDictionary<string, string> cabecalho = null)
        {
            var resposta = await this.PostSemToken<dynamic>(url, dados, cabecalho, this.MontarConteudoJson);
            return JsonConvert.DeserializeObject<TRetorno>(resposta);
        }

        public async Task<TRetorno> PostFormUrlEncoded<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao = new KeyValuePair<string, string>(), IDictionary<string, string> cabecalho = null)
        {
            var resposta = await this.Post<dynamic>(url, dados, tokenDeAutorizacao, cabecalho, this.MontarConteudoFormUrlEncoded);
            return JsonConvert.DeserializeObject<TRetorno>(resposta);
        }

        public async Task<TRetorno> PutJson<T, TRetorno>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao = default(KeyValuePair<string, string>), IDictionary<string, string> cabecalho = null)
        {
            var resposta = await this.Put<dynamic>(url, dados, tokenDeAutorizacao, cabecalho, this.MontarConteudoJson);
            return JsonConvert.DeserializeObject<TRetorno>(resposta);
        }

        private async Task<string> Put<T>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao, IDictionary<string, string> cabecalho,
            Func<object, HttpContent> montarConteudo)
        {
            this.DefinirTokenDeAutorizacao(tokenDeAutorizacao);
            this.MontarCabecalho(cabecalho);

            using (var conteudo = montarConteudo.Invoke(dados))
            {
                try
                {
                    using (var resposta = await this._clienteHttp.PutAsync(url, conteudo))
                    {
                        if (!resposta.IsSuccessStatusCode)
                            throw new HttpException((int)resposta.StatusCode, resposta.ReasonPhrase, new HttpRequestException(resposta.ReasonPhrase));

                        return await resposta.Content.ReadAsStringAsync();
                    }
                }
                catch (HttpRequestException)
                {
                    throw new InvalidOperationException("Servidor indisponível");
                }
            }
        }

        private async Task<string> Post<T>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao, IDictionary<string, string> cabecalho,
            Func<object, HttpContent> montarConteudo)
        {
            this.DefinirTokenDeAutorizacao(tokenDeAutorizacao);
            this.MontarCabecalho(cabecalho);

            using (var conteudo = montarConteudo.Invoke(dados))
            {
                try
                {
                    using (var resposta = await this._clienteHttp.PostAsync(url, conteudo))
                    {
                        if (!resposta.IsSuccessStatusCode)
                            throw new HttpException((int)resposta.StatusCode, resposta.ReasonPhrase, new HttpRequestException(resposta.ReasonPhrase));

                        return await resposta.Content.ReadAsStringAsync();
                    }
                }
                catch (HttpRequestException)
                {
                    throw new InvalidOperationException("Servidor indisponível");
                }
            }
        }

        private async Task<string> PostSemToken<T>(Uri url, T dados, IDictionary<string, string> cabecalho,
            Func<object, HttpContent> montarConteudo)
        {
            this.MontarCabecalho(cabecalho);
            
            using (var conteudo = montarConteudo.Invoke(dados))
            {
                try
                {
                    using (var resposta = await this._clienteHttp.PostAsync(url, conteudo))
                    {
                        if (!resposta.IsSuccessStatusCode)
                            throw new HttpException((int)resposta.StatusCode, resposta.ReasonPhrase, new HttpRequestException(resposta.ReasonPhrase));

                        var respostaContent = resposta.Content;

                        var conteudoDaResposta = resposta.Headers.ToList().FirstOrDefault(a => a.Key == "Token");
                        var dataExpiracao = resposta.Headers.ToList().FirstOrDefault(a => a.Key == "expirationDate");

                        var Token = conteudoDaResposta.Value.FirstOrDefault();
                        var DataExpiration = dataExpiracao.Value.FirstOrDefault();

                        Dictionary<string, string> values = new Dictionary<string, string>();
                        values.Add("Token", Token);
                        values.Add("DataExpiration", DataExpiration);
                        
                        return JsonConvert.SerializeObject(values);
                    }
                }
                catch (HttpRequestException)
                {
                    throw new InvalidOperationException("Servidor indisponível");
                }
            }
        }

        private async Task<string> PostGeral<T>(Uri url, T dados, KeyValuePair<string, string> tokenDeAutorizacao, IDictionary<string, string> cabecalho)
        {
            try
            {
                this._clienteHttp.BaseAddress = url;
                this._clienteHttp.DefaultRequestHeaders.Accept.Clear();
                this._clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenDeAutorizacao.Key, tokenDeAutorizacao.Value);

                var LisKey = cabecalho.ToList<KeyValuePair<string, string>>();
                var content = new FormUrlEncodedContent(LisKey);
                
                using (var resposta = await this._clienteHttp.PostAsync(url, content))
                {
                    if (!resposta.IsSuccessStatusCode)
                        throw new HttpException((int)resposta.StatusCode, resposta.ReasonPhrase, new HttpRequestException(resposta.ReasonPhrase));
                    
                    return await resposta.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException)
            {
                throw new InvalidOperationException("Servidor indisponível");
            }
            
        }

        public void Dispose()
        {
            if (this._clienteHttp != null)
                this._clienteHttp.Dispose();

            GC.SuppressFinalize(this);
        }

        private void DefinirTokenDeAutorizacao(KeyValuePair<string, string> tokenDeAutorizacao)
        {
            if (string.IsNullOrEmpty(tokenDeAutorizacao.Key))
                return;

            this._clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenDeAutorizacao.Key, tokenDeAutorizacao.Value);
        }

        private HttpContent MontarConteudoJson<T>(T conteudo)
        {
            if (conteudo == null)
                return new StringContent("");

            return new StringContent(JsonConvert.SerializeObject(conteudo), Encoding.UTF8, ApplicationJson);
        }

        private HttpContent MontarConteudoFormUrlEncoded<T>(T conteudo)
        {
            if (conteudo == null)
                return new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());

            var propriedades = conteudo.GetType().GetProperties();

            var conteudoFormatado = conteudo.GetType().GetProperties().Select(p => new KeyValuePair<string, string>(p.Name,
                p.GetValue(conteudo).ToString()));
            return new FormUrlEncodedContent(conteudoFormatado);
        }

        private void MontarCabecalho(IDictionary<string, string> cabecalho)
        {
            if (cabecalho == null || !cabecalho.Any())
                return;

            this._clienteHttp.DefaultRequestHeaders.Clear();

            if (cabecalho != null)
            {
                foreach (var linha in cabecalho)
                {
                    this._clienteHttp.DefaultRequestHeaders.Add(linha.Key, linha.Value);
                }
            }
        }
    }
}
