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

        public IList<Batalhao> RetornarTodosOsBatalhoes(string nome, int comandoRegional, int cidade, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Batalhao>()
                .Include(a => a.ComandoRegional)
                .Include(a => a.Cidade)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if(comandoRegional > 0)
                query = query.Where(c => c.ComandoRegional.Id == comandoRegional);

            if (cidade > 0)
                query = query.Where(c => c.Cidade.Id == cidade);

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(a => a.Sigla).ToList();
        }

        public IList<Batalhao> RetornarTodosOsBatalhoesAtivos()
        {
            var query = this._contexto.Set<Batalhao>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Sigla).ToList();
        }

        public Batalhao PegarPorId(int id)
        {
            return this._contexto.Set<Batalhao>()
                .Include(a => a.ComandoRegional)
                .Include(a => a.Cidade)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
