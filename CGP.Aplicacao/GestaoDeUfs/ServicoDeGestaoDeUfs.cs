using Cgp.Dominio.Entidades;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUfs
{
    public class ServicoDeGestaoDeUfs : IServicoDeGestaoDeUfs
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeUfs(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public IList<Uf> RetonarTodasUfs()
        {
            var ufs = this._servicoExternoDePersistencia.RepositorioDeUfs.RetornarUfsAtivas();
            return ufs;
        }
    }
}
