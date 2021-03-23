using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCidades : IRepositorioDeCidades
    {
        protected readonly Contexto _contexto;
        public RepositorioDeCidades(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public IList<Cidade> RetornarCidadesPorUf(int uf)
        {
            var query = this._contexto.Set<Cidade>().AsQueryable();
            query = query.Where(c => c.Uf.Id == uf);
            return query.OrderBy(a => a.Descricao).ToList();
        }

        public Cidade PegarPorId(int id)
        {
            return this._contexto.Set<Cidade>().Include(a => a.Uf).FirstOrDefault(a => a.Id == id);
        }
    }
}
