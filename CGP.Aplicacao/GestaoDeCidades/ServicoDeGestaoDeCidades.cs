using Cgp.Dominio.Entidades;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCidades
{
    public class ServicoDeGestaoDeCidades : IServicoDeGestaoDeCidades
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeCidades(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public IList<Cidade> RetonarCidadesPorUf(int uf)
        {
            var cidades = this._servicoExternoDePersistencia.RepositorioDeCidades.RetornarCidadesPorUf(uf);
            return cidades;
        }
    }
}
