using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeTemporada : Repositorio<Temporada>, IRepositorioDeTemporadas
    {
        public RepositorioDeTemporada(Contexto contexto) : base(contexto) { }


        public IList<Temporada> RetornarTemporadasPorFiltro(string nome, string ano, string pais, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Temporada>().AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(ano))
                query = query.Where(c => c.Ano.Contains(ano));

            if (!string.IsNullOrEmpty(pais))
                query = query.Where(c => c.Pais.Contains(pais));

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(a => a.Nome).ToList();
        }

        public Temporada PegarPorId(int id)
        {
            return this._contexto.Set<Temporada>().FirstOrDefault(a => a.Id == id);
        }

        public IList<Temporada> RetornarTodosAsTemporadasAtivas()
        {
            var query = this._contexto.Set<Temporada>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.ToList();
        }

        public Temporada BuscarTemporadaAtiva()
        {
            return this._contexto.Set<Temporada>().FirstOrDefault(a => a.Aberta);
        }
    }
}
