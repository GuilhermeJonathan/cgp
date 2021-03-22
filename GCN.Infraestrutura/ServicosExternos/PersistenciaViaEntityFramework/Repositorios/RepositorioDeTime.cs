using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeTime : Repositorio<Time>, IRepositorioDeTimes
    {
        public RepositorioDeTime(Contexto contexto) : base(contexto) { }

        public IList<Time> RetornarTodosOsTimes(string nome, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Time>().AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();
            return query.OrderBy(a => a.Nome).ToList();
        }

        public IList<Time> RetornarTodosOsTimesAtivos()
        {
            var query = this._contexto.Set<Time>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Nome).ToList();
        }

        public Time PegarPorId(int id)
        {
            return this._contexto.Set<Time>().FirstOrDefault(a => a.Id == id);
        }
    }
}
