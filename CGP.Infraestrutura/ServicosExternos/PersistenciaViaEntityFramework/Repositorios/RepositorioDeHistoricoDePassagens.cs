using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeHistoricoDePassagens : Repositorio<HistoricoDePassagem>, IRepositorioDeHistoricoDePassagens
    {
        public RepositorioDeHistoricoDePassagens(Contexto contexto) : base(contexto) { }

    }
}
