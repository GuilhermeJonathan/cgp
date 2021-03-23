using Cgp.Aplicacao.GestaoDeDashboard.Modelos;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeDashboard
{
    public class ServicoDeGestaoDeDashboard : IServicoDeGestaoDeDashboard
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public ServicoDeGestaoDeDashboard(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }
        
    }
}
