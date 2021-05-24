using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDePossuidores : Repositorio<Possuidor>, IRepositorioDePossuidores
    {
        public RepositorioDePossuidores(Contexto contexto) : base(contexto) { }

        public Possuidor PegarPorDocumento(string documento)
        {
            return this._contexto.Set<Possuidor>()
                .FirstOrDefault(a => a.Documento == documento);
        }
    }
}
