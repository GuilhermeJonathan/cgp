using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeComandosRegionais : Repositorio<ComandoRegional>, IRepositorioDeComandosRegionais
    {
        public RepositorioDeComandosRegionais(Contexto contexto) : base(contexto) { }

        public IList<ComandoRegional> RetornarTodosOsComandosRegionaisPorFiltro(string nome, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<ComandoRegional>().AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();
            return query.OrderBy(a => a.Nome).ToList();
        }

        public IList<ComandoRegional> RetornarTodosOsComandosRegionaisAtivos()
        {
            var query = this._contexto.Set<ComandoRegional>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Nome).ToList();
        }

        public ComandoRegional PegarPorId(int id)
        {
            return this._contexto.Set<ComandoRegional>().FirstOrDefault(a => a.Id == id);
        }
    }
}
