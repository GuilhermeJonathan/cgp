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

        public IList<Carater> RetornarCaratersPorFiltro(string placa, int[] cidades, int[] crimes, int situacao, DateTime? dataInicial, DateTime? dataFinal, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .AsQueryable();
            
            if(!String.IsNullOrEmpty(placa))
                query = query.Where(c => c.Veiculo.Placa.Contains(placa));

            if (cidades != null)
                query = query.Where(c => cidades.Contains(c.Cidade.Id));

            if (crimes != null)
                query = query.Where(c => crimes.Contains(c.Crime.Id));

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
                .Include(a => a.Fotos)
                .Include(a => a.HistoricosDeCaraters)
                .Include(a => a.HistoricosDeCaraters.Select(b => b.Usuario))
                .Include(a => a.HistoricosDePassagens)
                .FirstOrDefault(a => a.Id == id);
        }

        public IList<Carater> BuscarCaratersPorFragmentos(string fragmento)
        {
            var query = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .AsQueryable();

            if (!String.IsNullOrEmpty(fragmento))
                query = query.Where(c => c.Veiculo.Placa.Contains(fragmento) || c.Veiculo.Marca.Contains(fragmento) || c.Veiculo.Modelo.Contains(fragmento) || c.Veiculo.Cor.Contains(fragmento));
            

            return query.OrderByDescending(a => a.DataHoraDoFato).ToList();
        }

        public Carater PegarCaraterPorPlaca(string placa)
        {
            var carater = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .FirstOrDefault(a => a.Veiculo.Placa == placa && a.SituacaoDoCarater == SituacaoDoCarater.Cadastrado);

            return carater != null ? carater : null;
        }

        public Foto PegarFotoPorId(int id)
        {
            return this._contexto.Set<Foto>()
                .Include(a => a.Carater)
                .FirstOrDefault(a => a.Id == id);
        }

        public IList<Carater> RetornarTodosCaraters()
        {
            var query = this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .AsQueryable();

            query = query.Where(c => c.SituacaoDoCarater == SituacaoDoCarater.Cadastrado);

            return query.OrderByDescending(a => a.DataHoraDoFato).ToList();
        }

        public HistoricoDePassagem PegarHistoricoDePassagem(int id)
        {
            var historico = this._contexto.Set<HistoricoDePassagem>()
                .Include(a => a.Carater)
                .FirstOrDefault(a => a.Id == id);
            return historico;
        }

        public List<Alerta> PegarNovosAlertas()
        {
            var query = this._contexto.Set<Alerta>()
                .Include(a => a.HistoricoDePassagem)
                .Include(a => a.HistoricoDePassagem.Carater)
                .Include(a => a.HistoricoDePassagem.Carater.Veiculo)
                .AsQueryable();

            query = query.Where(c => c.SituacaoDoAlerta == SituacaoDoAlerta.Cadastrado);

            return query.OrderBy(a => a.DataDoCadastro).ToList();
        }

        public List<AlertaUsuario> PegarAlertasUsuarios(int idUsuario, int idAlerta)
        {
            var query = this._contexto.Set<AlertaUsuario>()
                .Include(a => a.Alerta)
                .Include(a => a.Usuario)
                .AsQueryable();

            query = query.Where(c => c.Usuario.Id == idUsuario && c.Alerta.Id == idAlerta);

            return query.OrderByDescending(a => a.DataDoCadastro).ToList();
        }

        public Alerta PegarAlertaPorId(int idCarater)
        {
            var alerta = this._contexto.Set<Alerta>()
                .Include(a => a.HistoricoDePassagem)
                .Include(a => a.HistoricoDePassagem.Carater)
                .FirstOrDefault(a => a.HistoricoDePassagem.Carater.Id == idCarater);
            return alerta;
        }

        public List<Alerta> PegarTodosAlertaPorCarater(int idCarater)
        {
            var alertas = this._contexto.Set<Alerta>()
                .Include(a => a.HistoricoDePassagem)
                .Include(a => a.HistoricoDePassagem.Carater)
                .Where(a => a.HistoricoDePassagem.Carater.Id == idCarater);
            return alertas.ToList();
        }
    }
}
