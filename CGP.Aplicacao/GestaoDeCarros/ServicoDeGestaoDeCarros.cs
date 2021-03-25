using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCarros
{
    public class ServicoDeGestaoDeCarros : IServicoDeGestaoDeCarros
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeCarros(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

    }
}
