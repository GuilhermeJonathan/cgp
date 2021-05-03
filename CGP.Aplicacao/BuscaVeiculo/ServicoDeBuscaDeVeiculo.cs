using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using Cgp.Aplicacao.ComunicacaoViaHttp;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo
{
    public class ServicoDeBuscaDeVeiculo : IServicoDeBuscaDeVeiculo
    {
        private readonly IServicoDeComunicacaoViaHttp _servicoHttp;
        private readonly string _urlToken;
        private readonly string _urlDaApi;
        private readonly string _urlDaApiCompleta;
        public ServicoDeBuscaDeVeiculo(IServicoDeComunicacaoViaHttp servicoHttp, string urlToken, string url, string urlCompleta)
        {
            this._urlToken = urlToken;
            this._servicoHttp = servicoHttp;
            this._urlDaApi = url;
            this._urlDaApiCompleta = urlCompleta;
        }

        public async Task<ModeloDeBuscaDeVeiculo> BuscarPlacaSimples(string placa)
        {
            try
            {
                return await this._servicoHttp.Get<ModeloDeBuscaDeVeiculo>(new Uri($"{this._urlDaApi}/{placa}"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModeloDeBuscaCompleto> BuscarPlacaComleta(string placa, UsuarioLogado usuario)
        {
            try
            {
                var token = await this.Autorizar();
                Dictionary<string, string> usuarioParametro = new Dictionary<string, string>();
                usuarioParametro.Add("usuario", usuario.Cpf);
                return await this._servicoHttp.Get<ModeloDeBuscaCompleto>(new Uri($"{this._urlDaApiCompleta}{placa}"), null, new KeyValuePair<string, string>("Bearer", token.Token.Replace("Bearer ", "")), usuarioParametro);
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        private async Task<ModeloDeRespostaDaAutorizacao> Autorizar()
        {
            return await this._servicoHttp.PostJsonSemToken<ModeloDeAutorizacao, ModeloDeRespostaDaAutorizacao>(
                    new Uri($"{this._urlToken}"), new ModeloDeAutorizacao());
        }

    }
}
