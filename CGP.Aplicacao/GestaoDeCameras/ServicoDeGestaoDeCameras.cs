using Cgp.Aplicacao.GestaoDeCameras.Mdelos;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras
{
    public class ServicoDeGestaoDeCameras : IServicoDeGestaDeCameras
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeCameras(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeCameraDaLista BuscarHistoricoDePassagem(string endereco)
        {
            try
            {
                var camera = this._servicoExternoDePersistencia.RepositorioDeCameras.PegarPorEndereco(endereco);
                if (camera == null)
                    new ExcecaoDeAplicacao("Câmera não encontrada.");

                var modelo = new ModeloDeCameraDaLista(camera);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar camêra");
            }
        }
    }
}
