using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeJogo : Repositorio<Jogo>, IRepositorioDeJogos
    {
        public RepositorioDeJogo(Contexto contexto) : base(contexto) { }

        public IList<Jogo> RetornarJogosPorFiltro(int time, int rodada, DateTime dataDoJogo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Jogo>().Include(nameof(Jogo.Time1))
                .Include(nameof(Jogo.Time2))
                .Include(a => a.Estadio)
                .Include(a => a.Rodada)
                .AsQueryable();

            if (time > 0)
                query = query.Where(c => c.Time1.Id == time || c.Time2.Id == time);

            if (rodada > 0)
                query = query.Where(c => c.Rodada.Id == rodada);

            if (dataDoJogo != DateTime.MinValue)
                query = query.Where(c => c.DataHoraDoJogo >= dataDoJogo && c.DataHoraDoJogo <= dataDoJogo);

            quantidadeEncontrada = query.Count();
            return query.OrderBy(a => a.Rodada.SituacaoDaRodada).ThenBy( b => b.Rodada.Ordem).ToList();
        }

        public IList<Jogo> RetornarTodosJogos()
        {
            var query = this._contexto.Set<Jogo>().Include(nameof(Jogo.Time1))
                .Include(nameof(Jogo.Time2))
                .Include(a => a.Estadio)
                .Include(a => a.Rodada)
                .AsQueryable();

            return query.OrderBy(a => a.DataDoCadastro).ToList();
        }

        public IList<Jogo> RetornarJogosPorRodada(int rodada)
        {
            var query = this._contexto.Set<Jogo>().Include(nameof(Jogo.Time1))
                .Include(nameof(Jogo.Time2))
                .Include(a => a.Estadio)
                .Include(a => a.Rodada)
                .AsQueryable();

            if (rodada > 0)
                query = query.Where(c => c.Rodada.Id == rodada);

            return query.OrderBy(a => a.DataHoraDoJogo).ToList();
        }

        public Jogo PegarPorId(int id)
        {
            return this._contexto.Set<Jogo>()
                .Include(nameof(Jogo.Time1))
                .Include(nameof(Jogo.Time2))
                .Include(a => a.Estadio)
                .Include(a => a.Rodada)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
