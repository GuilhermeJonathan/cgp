using Campeonato.Aplicacao.Comum;
using Campeonato.Aplicacao.GestaoDeRodada.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeRodada
{
    public class ServicoDeGestaoDeRodadas : IServicoDeGestaoDeRodadas
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;

        public ServicoDeGestaoDeRodadas(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeRodadas RetonarTodosasRodadas(ModeloDeFiltroDeRodada filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var rodadas = this._servicoExternoDePersistencia.RepositorioDeRodadas.RetornarTodosAsRodadas(filtro.Nome, filtro.Temporada, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeRodadas(rodadas, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar rodadas.");
            }
        }

        public ModeloDeEdicaoDeRodada BuscarRodadaPorId(int id)
        {
            try
            {
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(id);
                return new ModeloDeEdicaoDeRodada(rodada);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar rodada");
            }
        }

        public string CadastrarRodada(ModeloDeCadastroDeRodada modelo, UsuarioLogado usuario)
        {
            try
            {
                var novaRodada = new Rodada(modelo.Nome, modelo.Temporada);
                this._servicoExternoDePersistencia.RepositorioDeRodadas.Inserir(novaRodada);
                this._servicoExternoDePersistencia.Persistir();

                return "Rodada incluída com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir rodada: " + ex.InnerException);
            }
        }

        public string AlterarDadosDaRodada(ModeloDeEdicaoDeRodada modelo, UsuarioLogado usuario)
        {
            try
            {
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(modelo.Id);
                rodada.AlterarRodada(modelo.Nome, modelo.Temporada, modelo.Ativo);
                this._servicoExternoDePersistencia.Persistir();

                return "Rodada alterada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar a rodada: " + ex.InnerException);
            }

        }
    }
}
