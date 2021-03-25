using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCrimes : Repositorio<Crime>, IRepositorioDeCrimes
    {
        public RepositorioDeCrimes(Contexto contexto) : base(contexto) { }
        public Crime PegarPorId(int id)
        {
            return this._contexto.Set<Crime>()
                .FirstOrDefault(a => a.Id == id);
        }

        public IList<Crime> RetornarCrimesPorFiltro(string nome, string artigo, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Crime>()
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(artigo))
                query = query.Where(c => c.Artigo.Contains(artigo));
            
            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(a => a.Artigo).ToList();
        }

        public IList<Crime> RetornarTodosOsCrimesAtivos()
        {
            var query = this._contexto.Set<Crime>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Artigo).ToList();
        }
    }
}
