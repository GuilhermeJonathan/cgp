using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeRodada : Repositorio<Rodada>, IRepositorioDeRodadas
    {
        public RepositorioDeRodada(Contexto contexto) : base(contexto) { }

        public IList<Rodada> RetornarTodosAsRodadas(string nome, string temporada, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Rodada>().AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(temporada))
                query = query.Where(c => c.Nome.Contains(temporada));

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(a => a.Nome).ToList();
        }

        public Rodada PegarPorId(int id)
        {
            return this._contexto.Set<Rodada>().FirstOrDefault(a => a.Id == id);
        }

    }
}
