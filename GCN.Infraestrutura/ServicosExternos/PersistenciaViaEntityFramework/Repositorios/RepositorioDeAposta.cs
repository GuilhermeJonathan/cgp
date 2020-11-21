using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Dominio.Repositorios;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeAposta : Repositorio<Aposta>, IRepositorioDeApostas
    {
        public RepositorioDeAposta(Contexto contexto) : base(contexto) { }

        public IList<Aposta> RetornarApostasPorFiltro(int usuario, int rodada, int tipoDeAposta, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Aposta>()
                .Include(a => a.Usuario)
                .Include(a => a.Rodada)
                .Include(a => a.Jogos)
                .Include(a => a.Jogos.Select(b => b.Time1))
                .Include(a => a.Jogos.Select(b => b.Time2)).AsQueryable();

            if (usuario > 0)
                query = query.Where(c => c.Usuario.Id == usuario);

            if (rodada > 0)
                query = query.Where(c => c.Rodada.Id == rodada);
            
            if(tipoDeAposta > 0)
                query = query.Where(c => (int)c.TipoDeAposta == tipoDeAposta);

            quantidadeEncontrada = query.Count();
            return query.OrderBy(a => a.Rodada.Ordem).ThenBy(b => b.Rodada.SituacaoDaRodada).ToList();
        }

        public IList<Aposta> RetornarApostasPorRodada(int rodada)
        {
            var query = this._contexto.Set<Aposta>()
                .Include(a => a.Usuario)
                .Include(a => a.Rodada)
                .Include(a => a.Jogos)
                .Include(a => a.Jogos.Select(b => b.Estadio))
                .Include(a => a.Jogos.Select(b => b.Time1))
                .Include(a => a.Jogos.Select(b => b.Time2)).AsQueryable();

            if (rodada > 0)
                query = query.Where(c => c.Rodada.Id == rodada);

            return query.ToList();
        }

        public Aposta PegarPorId(int id)
        {
            return this._contexto.Set<Aposta>()
                .Include(a => a.Usuario)
                .Include(a => a.Jogos)
                .Include(a => a.Jogos.Select(b => b.Estadio))
                .Include(a => a.Jogos.Select(b => b.Time1))
                .Include(a => a.Jogos.Select(b => b.Time2))
                .Include(a => a.Rodada)
                .FirstOrDefault(a => a.Id == id);
        }

        public Aposta PegarPorIdRodadaEUsuario(int id, int usuario)
        {
           return this._contexto.Set<Aposta>()
                .Include(a => a.Usuario)
                .Include(a => a.Jogos)
                .Include(a => a.Jogos.Select(b => b.Estadio))
                .Include(a => a.Jogos.Select(b => b.Time1))
                .Include(a => a.Jogos.Select(b => b.Time2))
                .Include(a => a.Rodada)
                .FirstOrDefault(a => a.Rodada.Id == id && a.Usuario.Id == usuario);
        }

        public IList<Aposta> RetornarApostasParaResultado(int idRodada)
        {
            var query = this._contexto.Set<Aposta>()
                .Include(a => a.Usuario)
                .Include(a => a.Rodada)
                .Include(a => a.Jogos)
                .Include(a => a.Jogos.Select(b => b.Time1))
                .Include(a => a.Jogos.Select(b => b.Time2)).AsQueryable();

            if (idRodada > 0)
                query = query.Where(c => c.Rodada.Id == idRodada);

            query = query.Where(c => c.TipoDeAposta == TipoDeAposta.Geral);

            return query.ToList();
        }

        public IList<JogoDaAposta> RetornarJogosDaApostaParaAlterar(int idRodada, int time1, int time2)
        {
            var query = this._contexto.Set<JogoDaAposta>()
                .Include(a => a.Time1)
                .Include(a => a.Time2).AsQueryable();

            query = query.Where(c => c.Rodada.Id == idRodada);
            query = query.Where(c => c.Time1.Id == time1 && c.Time2.Id == time2);

            return query.ToList();
        }


        public Aposta PegarRodadaExclusiva( int idUsuario, int idRodada = 0)
        {
            var query = this._contexto.Set<Aposta>()
                .Include(a => a.Usuario)
                .Include(a => a.Jogos)
                .Include(a => a.Jogos.Select(b => b.Estadio))
                .Include(a => a.Jogos.Select(b => b.Time1))
                .Include(a => a.Jogos.Select(b => b.Time2))
                .Include(a => a.Rodada).AsQueryable();

            query = query.Where(c => c.TipoDeAposta == TipoDeAposta.Exclusiva);

            if(idRodada > 0)
                query = query.Where(a => a.Rodada.Id == idRodada);

            return query.FirstOrDefault(a => a.Usuario.Id == idUsuario );
        }
    }
}
