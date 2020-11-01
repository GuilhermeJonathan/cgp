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

        public IList<Rodada> RetornarTodosAsRodadas(int rodada, string temporada, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Rodada>()
                .AsQueryable();

            if (rodada > 0)
                query = query.Where(c => c.Id == rodada);

            if (!string.IsNullOrEmpty(temporada))
                query = query.Where(c => c.Temporada.Contains(temporada));

            quantidadeEncontrada = query.Count();

            var rodadas = query.OrderBy(a => a.SituacaoDaRodada).ThenBy(b => b.Ordem).ToList();
            return rodadas;
        }

        public IList<Rodada> RetornarTodosAsRodadasAtivas()
        {
            var query = this._contexto.Set<Rodada>().AsQueryable();
            query = query.Where(c => c.Ativo);
            return query.OrderBy(a => a.Ordem).ToList();
        }

        public Rodada PegarPorId(int id)
        {
            return this._contexto.Set<Rodada>().FirstOrDefault(a => a.Id == id);
        }

        public int BuscarRodadaAtiva()
        {
            return this._contexto.Set<Rodada>().FirstOrDefault(a => a.SituacaoDaRodada == Dominio.ObjetosDeValor.SituacaoDaRodada.Atual).Id;
        }

        public Rodada BuscarProximaRodada()
        {
            return this._contexto.Set<Rodada>().FirstOrDefault(a => a.SituacaoDaRodada == Dominio.ObjetosDeValor.SituacaoDaRodada.Futura);
        }

    }
}
