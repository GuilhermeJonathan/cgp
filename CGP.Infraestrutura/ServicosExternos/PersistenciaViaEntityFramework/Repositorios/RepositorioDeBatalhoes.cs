using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeBatalhoes : Repositorio<Batalhao>, IRepositorioDeBatalhoes
    {
        public RepositorioDeBatalhoes(Contexto contexto) : base(contexto) { }

        public IList<Batalhao> RetornarTodosOsBatalhoes(string nome, int comandoRegional, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Batalhao>()
                .Include(a => a.ComandoRegional)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if(comandoRegional > 0)
                query = query.Where(c => c.ComandoRegional.Id == comandoRegional);

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(a => a.Nome).ToList();
        }

        public IList<Batalhao> RetornarTodosOsBatalhoesAtivos()
        {
            var query = this._contexto.Set<Batalhao>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Nome).ToList();
        }

        public Batalhao PegarPorId(int id)
        {
            return this._contexto.Set<Batalhao>().Include(a => a.ComandoRegional).FirstOrDefault(a => a.Id == id);
        }
    }
}
