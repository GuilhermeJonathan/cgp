using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeEstadio : Repositorio<Estadio>, IRepositorioDeEstadios
    {
        public RepositorioDeEstadio(Contexto contexto) : base(contexto) { }

        public IList<Estadio> RetornarTodosOsEstadios(string nome, int time, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Estadio>().Include(nameof(Estadio.Time)).AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if(time > 0)
                query = query.Where(c => c.Time.Id == time);

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();
            return query.OrderBy(a => a.Nome).ToList();
        }

        public IList<Estadio> RetornarTodosOsEstadiosAtivos()
        {
            var query = this._contexto.Set<Estadio>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Nome).ToList();
        }

        public Estadio PegarPorId(int id)
        {
            return this._contexto.Set<Estadio>().Include(nameof(Estadio.Time)).FirstOrDefault(a => a.Id == id);
        }
    }
}
