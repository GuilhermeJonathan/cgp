using Cgp.Aplicacao.GestaoDeCameras;
using Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeHistoricoDePassagens
{
    public class ServicoDeGestaoDeHistoricoDePassagens : IServicoDeHistoricoDePassagens
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGestaDeCameras _servicoDeGestaoDeCameras;

        public ServicoDeGestaoDeHistoricoDePassagens(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia,
            IServicoDeGestaDeCameras servicoDeGestaoDeCameras)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGestaoDeCameras = servicoDeGestaoDeCameras;
        }

        public ModeloDeListaDeHistoricoDePassagens RetornarHistociosDePassagensPorFiltro(ModeloDeFiltroDeHistoricoDePassagem filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var passagens = this._servicoExternoDePersistencia.RepositorioDeCaraters.RetornarHistoricosPassagensPorFiltro(
                    filtro.Placa, filtro.CidadesSelecionadas, filtro.CrimesSelecionados, filtro.SituacaoDoCarater, out quantidadeEncontrada);
                
                var cameras = _servicoDeGestaoDeCameras.BuscarCamerasAtivas();

                return new ModeloDeListaDeHistoricoDePassagens(passagens, cameras, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os históricos de passagens");
            }
        }
    }
}
