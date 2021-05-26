using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using Cgp.Aplicacao.ComunicacaoViaHttp;
using Cgp.Aplicacao.Util;
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
        private readonly string _urlDaApiCompleta;
        private readonly string _urlDaApiProprietario;

        public ServicoDeBuscaDeVeiculo(IServicoDeComunicacaoViaHttp servicoHttp, string urlToken, string urlCompleta)
        {
            this._urlToken = urlToken;
            this._servicoHttp = servicoHttp;
            this._urlDaApiCompleta = urlCompleta;
            this._urlDaApiProprietario = VariaveisDeAmbiente.Pegar<string>("Cortex:urlBuscaProprietario");
        }

        public async Task<ModeloDeBuscaDeVeiculo> BuscarPlacaSimples(string placa)
        {
            try
            {
                return await this._servicoHttp.Get<ModeloDeBuscaDeVeiculo>(new Uri($"{this._urlDaApiCompleta}/{placa}"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModeloDeBuscaCompleto> BuscarPlacaCompleta(string placa, UsuarioLogado usuario)
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

        public async Task<ModeloDeListaDeBuscas> BuscarPlacasPorFiltro(ModeloDeFiltroDeBusca filtro, UsuarioLogado usuario)
        {
            try
            {
                var lista = new List<ModeloDeBuscaCompleto>();
                var token = await this.Autorizar();
                Dictionary<string, string> usuarioParametro = new Dictionary<string, string>();
                usuarioParametro.Add("usuario", usuario.Cpf);
                
                if (!String.IsNullOrEmpty(filtro.Placa))
                {
                    var veiculo = await this._servicoHttp.Get<ModeloDeBuscaCompleto>(new Uri($"{this._urlDaApiCompleta}{filtro.Placa}"), null, new KeyValuePair<string, string>("Bearer", token.Token.Replace("Bearer ", "")), usuarioParametro);
                    if (veiculo == null)
                        new ExcecaoDeAplicacao("Veículo não encontrado.");

                    lista.Add(veiculo);
                }

                else if (!String.IsNullOrEmpty(filtro.Cpf))
                {
                    var veiculos = await this._servicoHttp.Get<List<ModeloDeBuscaCompleto>>(new Uri($"{this._urlDaApiProprietario}{filtro.Cpf}"), null, new KeyValuePair<string, string>("Bearer", token.Token.Replace("Bearer ", "")), usuarioParametro);
                    if (veiculos == null)
                        new ExcecaoDeAplicacao("Veículo não encontrado.");

                    foreach (var veiculo in veiculos)
                        lista.Add(veiculo);
                }

                var modelo = new ModeloDeListaDeBuscas(lista, lista.Count, filtro);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os caráters");
            }
        }

        public async Task<ModeloDeBuscaDaLista> DetalharVeiculo(ModeloDeFiltroDeBusca filtro, UsuarioLogado usuario)
        {
            try
            {
                var veiculo = await this.BuscarPlacaCompleta(filtro.Placa, usuario);
                if(veiculo == null)
                    new ExcecaoDeAplicacao("Veículo não encontrado.");

                var modelo = new ModeloDeBuscaDaLista(veiculo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os caráters");
            }
        }
    }
}
