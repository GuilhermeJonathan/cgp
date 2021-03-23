using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeUfs : IRepositorioDeUfs
    {
        protected readonly Contexto _contexto;
        public RepositorioDeUfs(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public IList<Uf> RetornarUfsAtivas()
        {
            var query = this._contexto.Set<Uf>().AsQueryable();
            return query.OrderBy(a => a.Descricao).ToList();
        }
    }
}
