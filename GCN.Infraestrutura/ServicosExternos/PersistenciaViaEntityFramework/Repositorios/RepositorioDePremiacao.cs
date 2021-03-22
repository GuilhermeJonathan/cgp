using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDePremiacao : Repositorio<Premiacao>, IRepositorioDePremiacoes
    {
        public RepositorioDePremiacao(Contexto contexto) : base(contexto) { }

        public IList<Premiacao> RetornarPremiacoesPorFiltro(int rodada, int usuario, int pagina, int registrosPorPagina, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Premiacao>()
                .AsQueryable();

            if (rodada > 0)
                query = query.Where(c => c.Rodada.Id == rodada);

            if (usuario > 0)
                query = query.Where(c => c.PrimeiroColocado.Id == usuario || c.SegundoColocado.Id == usuario);

            quantidadeEncontrada = query.Count();

            return query.OrderByDescending(i => i.DataDoCadastro).Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
        }

        public IList<Premiacao> RetornarPremiacoesPorTemporada(int usuario)
        {
            var query = this._contexto.Set<Premiacao>()
                .AsQueryable();

            if (usuario > 0)
                query = query.Where(c => c.PrimeiroColocado.Id == usuario || c.SegundoColocado.Id == usuario);

            return query.OrderByDescending(i => i.DataDoCadastro).ToList();
        }

        public Premiacao PegarPorId(int id)
        {
            return this._contexto.Set<Premiacao>()
                .Include(b => b.Rodada)
                .Include(b => b.PrimeiroColocado)
                .Include(b => b.SegundoColocado)
                .Include(b => b.UsuarioQueGerou)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
