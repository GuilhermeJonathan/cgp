using Campeonato.Aplicacao.Comum;
using Campeonato.Aplicacao.GestaoDeTimes.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTimes
{
    public class ServicoDeGestaoDeTimes : IServicoDeGestaoDeTimes
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeTimes(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeTimes RetonarTodosOsTimes(ModeloDeFiltroDeTime filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var times = this._servicoExternoDePersistencia.RepositorioDeTimes.RetornarTodosOsTimes(filtro.Nome, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeTimes(times, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar times");
            }
        }

        public IList<Time> RetonarTodosOsTimesAtivos()
        {
            var times = this._servicoExternoDePersistencia.RepositorioDeTimes.RetornarTodosOsTimesAtivos();
            return times;
        }

        public ModeloDeEdicaoDeTime BuscarTimePorId(int id)
        {
            try
            {
                var time = this._servicoExternoDePersistencia.RepositorioDeTimes.PegarPorId(id);
                return new ModeloDeEdicaoDeTime(time);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar times");
            }
        }

        public string AlterarDadosDoTime(ModeloDeEdicaoDeTime modelo, UsuarioLogado usuario)
        {
            try
            {
                var time = this._servicoExternoDePersistencia.RepositorioDeTimes.PegarPorId(modelo.Id);
                time.AlterarDados(modelo.Nome, modelo.Ativo);
                this._servicoExternoDePersistencia.Persistir();

                return "Time alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o time: " + ex.InnerException);
            }
            
        }
    }
}
