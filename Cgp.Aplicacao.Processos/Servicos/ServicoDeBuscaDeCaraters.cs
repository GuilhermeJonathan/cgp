using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using Cgp.Aplicacao.ComunicacaoViaHttp;
using Cgp.Aplicacao.Processos.InterfaceDeServicos;
using Cgp.Aplicacao.Processos.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.ComunicacaoViaHttp;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Processos.Servicos
{
    public class ServicoDeBuscaDeCaraters : IServicoDeBuscaDeCaraters
    {
        private Contexto contexto = new Contexto();
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoDePersistencia;
        private readonly IServicoDeComunicacaoViaHttp _servicoHttp;
        private readonly string _urlToken;
        private readonly string _urlDaBusca;

        public ServicoDeBuscaDeCaraters(SqlConnection conexao)
        {
            this._servicoDePersistencia = new ServicoExternoDePersistenciaViaEntityFramework(contexto);
            this._servicoHttp = new ServicoDeComunicacaoViaHttp();
            this._urlToken = VariaveisDeAmbiente.Pegar<string>("Cortex:urlToken");
            this._urlDaBusca = VariaveisDeAmbiente.Pegar<string>("Cortex:urlBuscaVeiculo");
        }

        public async Task Executar()
        {
            Console.WriteLine($"*********** PROCESSO INICIADO");

            var usuarioBanco = this._servicoDePersistencia.RepositorioDeUsuarios.BuscarPorId(1);
            var caraters = this._servicoDePersistencia.RepositorioDeCaraters.RetornarTodosCaraters();
            var setentaEDuasHoras = DateTime.Now.AddDays(-3);
            var descricaoBaixa = $"Realizada baixa do caráter de forma automática.";
            var total = caraters.Count;
            var contador = 0;

            Console.WriteLine($"Lista de Caráters:{caraters.Count}");

            var token = await this.Autorizar();
            Console.WriteLine($"*********** TOKEN API: {token.Token}");

            Dictionary<string, string> usuarioParametro = new Dictionary<string, string>();
            usuarioParametro.Add("usuario", "02025032161");

            foreach (var carater in caraters)
            {
                contador++;
                Console.WriteLine($"Caráter {contador} de {total}: {carater.Veiculo.Placa}");
                var modelo = await this._servicoHttp.Get<ModeloDeBuscaCompleto>(new Uri($"{this._urlDaBusca}{carater.Veiculo.Placa}"), null, new KeyValuePair<string, string>("Bearer", token.Token.Replace("Bearer ", "")), usuarioParametro);

                if(modelo.restricao.Count == 0)
                {
                    //baixa automática em 72 horas
                    if (carater.DataDoCadastro < setentaEDuasHoras)
                    {
                        Console.WriteLine($"*********** VEÍCULO BAIXADO: {carater.Veiculo.Placa}");
                        carater.RealizarBaixaAutomatica(descricaoBaixa, usuarioBanco);
                        carater.RealizarBaixaAlertas();
                        carater.AdicionarHistorico(new HistoricoDeCarater("Realizou baixa do Caráter", descricaoBaixa, TipoDeHistoricoDeCarater.Baixa, usuarioBanco, carater.Id));
                    }
                }
            }

            Console.WriteLine($"*********** PROCESSO FINALIZADO");
            this._servicoDePersistencia.Persistir();
        }

        private async Task<ModeloDeRespostaDaAutorizacao> Autorizar()
        {
            return await this._servicoHttp.PostJsonSemToken<ModeloDeAutorizacao, ModeloDeRespostaDaAutorizacao>(
                    new Uri($"{this._urlToken}"), new ModeloDeAutorizacao());
        }
    }
}
