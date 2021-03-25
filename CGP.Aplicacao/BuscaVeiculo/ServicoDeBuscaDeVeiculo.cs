using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.ComunicacaoViaHttp;
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
        private readonly string _urlDaApi;
        public ServicoDeBuscaDeVeiculo(IServicoDeComunicacaoViaHttp servicoHttp, string url)
        {
            this._servicoHttp = servicoHttp;
            this._urlDaApi = url;
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
    }
}
