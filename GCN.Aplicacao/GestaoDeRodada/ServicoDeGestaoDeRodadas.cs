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
                var rodadas = this._servicoExternoDePersistencia.RepositorioDeRodadas.RetornarTodosAsRodadas(filtro.Rodada, filtro.Temporada, out quantidadeEncontrada);
                var jogosPorRodadas = this._servicoExternoDePersistencia.RepositorioDeJogos.RetornarTodosJogos();

                foreach (var rodada in rodadas)
                    rodada.Jogos = jogosPorRodadas.Where(a => a.Rodada.Id == rodada.Id).OrderBy(a => a.DataHoraDoJogo).ToList();
                
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

                if (rodada != null)
                {
                    var jogos = this._servicoExternoDePersistencia.RepositorioDeJogos.RetornarJogosPorRodada(rodada.Id);
                    rodada.Jogos = jogos;
                }

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

        public IList<Rodada> RetonarTodosAsRodadasAtivas()
        {
            var rodadas = this._servicoExternoDePersistencia.RepositorioDeRodadas.RetornarTodosAsRodadasAtivas();
            return rodadas;
        }

        public int BuscarRodadaAtiva()
        {
            var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.BuscarRodadaAtiva();
            return rodada;
        }

        public string CadastrarResultados(int id, int[] placar1, int[] placar2, int[] idJogos, UsuarioLogado usuario)
        {
            try
            {
                var contador = 0;
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                foreach (var idJogo in idJogos)
                {
                    var jogo = this._servicoExternoDePersistencia.RepositorioDeJogos.BuscarPorId(idJogo);
                   
                    if (jogo != null)
                    {
                        jogo.PlacarTime1 = placar1[contador];
                        jogo.PlacarTime2 = placar2[contador];

                        if(jogo.DataHoraDoJogo < DateTime.Now)
                            jogo.SituacaoDoJogo = SituacaoDoJogo.Finalizado;
                        else
                            jogo.SituacaoDoJogo = SituacaoDoJogo.AJogar;
                    }
                    contador++;
                }

                rodada.IncluirAlteracao(DateTime.Now, usuarioBanco);

                if (rodada.Jogos != null)
                {
                    var aindaTemJogo = rodada.Jogos.Any(a => a.SituacaoDoJogo == SituacaoDoJogo.AJogar);
                    if (!aindaTemJogo)
                    {
                        rodada.SituacaoDaRodada = SituacaoDaRodada.Finalizada;
                        var proximaRodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.BuscarProximaRodada();
                        proximaRodada.SituacaoDaRodada = SituacaoDaRodada.Atual;
                    }
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Resultados cadastrados com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir resultado: " + ex.InnerException);
            }
        }

    }
}
