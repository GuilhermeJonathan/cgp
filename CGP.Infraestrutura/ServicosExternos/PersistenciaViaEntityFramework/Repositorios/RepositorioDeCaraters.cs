using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Cgp.Dominio.ObjetosDeValor;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCaraters : Repositorio<Carater>, IRepositorioDeCaraters
    {
        public RepositorioDeCaraters(Contexto contexto) : base(contexto) { }

        public IList<Carater> RetornarCaratersPorFiltro(int cidade, int crime, int situacao, DateTime? dataInicial, DateTime? dataFinal, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .AsQueryable();
            
            if (cidade > 0)
                query = query.Where(c => c.Cidade.Id == cidade);

            if (crime > 0)
                query = query.Where(c => c.Crime.Id == crime);

            if (situacao > 0)
                query = query.Where(c => (int)c.SituacaoDoCarater == situacao);

            if (dataInicial.HasValue && dataFinal.HasValue)
                query = query.Where(a => a.DataHoraDoFato >= dataInicial.Value && a.DataHoraDoFato <= dataFinal.Value);

            quantidadeEncontrada = query.Count();

            return query.OrderByDescending(a => a.DataHoraDoFato).ToList();
        }

        public IList<Carater> RetornarCaratersPorCidades(int[] cidades, DateTime dataParaBusca)
        {
            var query = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .AsQueryable();

            if (cidades != null)
                query = query.Where(c => cidades.Contains(c.Cidade.Id));

            
            query = query.Where(c => c.SituacaoDoCarater == SituacaoDoCarater.Cadastrado && c.DataHoraDoFato > dataParaBusca);

            return query.OrderByDescending(a => a.DataHoraDoFato).ToList();
        }

        public Carater PegarPorId(int id)
        {
            return this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.CidadeLocalizado)
                .Include(a => a.Crime)
                .Include(a => a.UsuarioQueAlterou)
                .FirstOrDefault(a => a.Id == id);
        }

        public IList<Carater> BuscarCaratersPorPlaca(string placa)
        {
            var query = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .AsQueryable();

            if (!String.IsNullOrEmpty(placa))
                query = query.Where(c => c.Veiculo.Placa.Contains(placa));

            return query.OrderByDescending(a => a.DataHoraDoFato).ToList();
        }

        public Carater PegarCaraterPorPlaca(string placa)
        {
            var carater = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .FirstOrDefault(a => a.Veiculo.Placa == placa && a.SituacaoDoCarater == SituacaoDoCarater.Cadastrado);

            return carater != null ? carater : null;
        }

    }
}
