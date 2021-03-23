using Cgp.Dominio.Entidades;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais
{
    public class ServicoDeGestaoDeComandosRegionais : IServicoDeGestaoDeComandosRegionais
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeComandosRegionais(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public IList<ComandoRegional> RetonarTodosOsComandosRegionaisAtivos()
        {
            var comandos = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.RetornarTodosOsComandosRegionaisAtivos();
            return comandos;
        }
    }
}
