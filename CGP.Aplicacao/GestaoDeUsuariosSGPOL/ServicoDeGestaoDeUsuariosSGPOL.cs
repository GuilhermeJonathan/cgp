using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.ComunicacaoViaHttp;
using Cgp.Aplicacao.GestaoDeUsuariosSGPOL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios
{
    public class ServicoDeGestaoDeUsuariosSGPOL : IServicoDeGestaoDeUsuariosSGPOL
    {
        private readonly string _urlToken;
        private readonly string _urlBusca;
        private readonly IServicoDeComunicacaoViaHttp _servicoHttp;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;

        public ServicoDeGestaoDeUsuariosSGPOL(IServicoDeComunicacaoViaHttp servicoHttp, IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha, string urlToken, string urlBusca)
        {
            this._servicoHttp = servicoHttp;
            this._servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
            this._urlToken = urlToken;
            this._urlBusca = urlBusca;
        }

        public async Task<ModeloDeUsuarioSGPOL> BuscarDadosUsuario(string username, string password)
        {
            try
            {
                var usuario = new Dictionary<string, string> {
                    { "client_id", "appsgpol"  },
                    { "username", username  },
                    { "password", password },
                    { "grant_type", "password" }
                };
                var tokenRetorno = await this.Autorizar(usuario);

                return await this._servicoHttp.Get<ModeloDeUsuarioSGPOL>(new Uri($"{this._urlBusca}"), null, new KeyValuePair<string, string>("Bearer", tokenRetorno.access_token));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<ModeloDeRespostaDaAutorizacao> Autorizar(Dictionary<string, string> usuario)
        {
            var modeloAutorizacao = new ModeloDeAutorizacaoSGPOL();
            var token = modeloAutorizacao.TokenCriptografado(this._servicoDeGeracaoDeHashSha.EncodeToBase64);

            var retorno = await this._servicoHttp.PostJsonGeral<ModeloDeAutorizacaoSGPOL, ModeloDeRespostaDaAutorizacao>(
                    new Uri($"{this._urlToken}"), modeloAutorizacao, new KeyValuePair<string, string>("Basic", token), usuario);

            return retorno;

        }
    }
}
