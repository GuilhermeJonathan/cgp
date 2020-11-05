using Campeonato.Aplicacao.Util;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeApostas.Modelos
{
    public class ServicoDeGestaoDeApostas : IServicoDeGestaoDeApostas
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeApostas(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeApostas BuscarApostasPorFiltro(ModeloDeFiltroDeAposta filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var apostas = this._servicoExternoDePersistencia.RepositorioDeApostas.RetornarApostasPorFiltro(filtro.Usuario, filtro.Rodada, out quantidadeEncontrada);

                apostas = apostas.Where(a => a.Jogos.Count > 0).ToList();

                return new ModeloDeListaDeApostas(apostas, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar Apostas");
            }
        }

        public ModeloDeEdicaoDeAposta BuscarApostaPorRodada(int idRodada, UsuarioLogado usuario)
        {
            
            var aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarPorIdRodadaEUsuario(idRodada, usuario.Id);
                
            if (aposta != null)
            {
                if (aposta.Jogos == null || aposta.Jogos.Count == 0)
                {
                    var jogos = this._servicoExternoDePersistencia.RepositorioDeJogos.RetornarJogosPorRodada(aposta.Rodada.Id);
                    jogos.ToList().ForEach(a => aposta.Jogos.Add(new JogoDaAposta(a.DataHoraDoJogo, a.Time1, a.Time2, a.Rodada, a.Estadio, 0, 0)));
                }
            } else
            {
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(idRodada);
                if (rodada == null)
                    throw new ExcecaoDeAplicacao("Rodada não encontrada");

                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                var novaAposta = new Aposta(usuarioBanco, rodada);
                    
                var jogos = this._servicoExternoDePersistencia.RepositorioDeJogos.RetornarJogosPorRodada(rodada.Id);

                jogos.ToList().ForEach(a => novaAposta.Jogos.Add(new JogoDaAposta(a.DataHoraDoJogo, a.Time1, a.Time2, a.Rodada, a.Estadio, 0, 0)));

                this._servicoExternoDePersistencia.RepositorioDeApostas.Inserir(novaAposta);
                 
                aposta = novaAposta;
            }

            this._servicoExternoDePersistencia.Persistir();

            return new ModeloDeEdicaoDeAposta(aposta);
            
        }

        public ModeloDeEdicaoDeAposta VisualizarAposta(int idRodada, int idUsuario)
        {
            try
            {
                var aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarPorIdRodadaEUsuario(idRodada, idUsuario);
                
                return new ModeloDeEdicaoDeAposta(aposta);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar aposta");
            }
        }

        public string SalvarMinhaAposta(int id, int[] placar1, int[] placar2, int[] idJogos, UsuarioLogado usuario)
        {
            try
            {
                var contador = 0;

                var aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarPorIdRodadaEUsuario(id, usuario.Id);
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada)
                    throw new ExcecaoDeAplicacao("Rodada encontra-se finalizada.");

                if (rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada)
                {
                    if(rodada.Fechada)
                        throw new ExcecaoDeAplicacao("Rodada encontra-se fechada.");
                }
                    

                if(aposta.Rodada.DataPrimeiroJogo.AddMinutes(-VariaveisDeAmbiente.Pegar<int>("TempoParaFechamentoDeRodada")) < DateTime.Now)
                    throw new ExcecaoDeAplicacao("Rodada iniciada. Não é possível alterar as apostas.");

                if (aposta != null)
                {
                    if (aposta.Jogos != null)
                    {
                        foreach (var idJogo in idJogos)
                        {
                            var jogo = this._servicoExternoDePersistencia.RepositorioDeJogos.PegarJogoDaApostaPorId(idJogo);
                            jogo.PlacarTime1 = placar1[contador];
                            jogo.PlacarTime2 = placar2[contador];
                            contador++;
                        }
                    }
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Aposta salva com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível salvar aposta: " + ex.InnerException);
            }
        }

        public ModeloDeListaDeApostas BuscarResultado(int idRodada)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var apostas = this._servicoExternoDePersistencia.RepositorioDeApostas.RetornarApostasParaResultado(idRodada);

                apostas = apostas.Where(a => a.Jogos.Count > 0).ToList();

                var modelo = new ModeloDeListaDeApostas(apostas, quantidadeEncontrada, new ModeloDeFiltroDeAposta());

                var apostaAgrupada = apostas.GroupBy(l => l.Usuario.Id).Select(cl => new
                { 
                    Id = cl.First().Id,
                    Nome = cl.First().Usuario.Nome.Valor.ToString(),
                    AcertoPlacar = cl.Sum(c => c.AcertoPlacar),
                    AcertoEmpate = cl.Sum(c => c.AcertoEmpate),
                    AcertoGanhador = cl.Sum(c => c.AcertoGanhador),
                    Pontuacao = cl.Sum(c => c.Pontuacao),
                }).ToList();

                var contador = 1;
                var modeloFinal = new List<ModeloDeApostaDaLista>();

                apostaAgrupada = apostaAgrupada.OrderByDescending(a => a.AcertoGanhador).ToList();
                apostaAgrupada = apostaAgrupada.OrderByDescending(a => a.AcertoEmpate).ToList();
                apostaAgrupada = apostaAgrupada.OrderByDescending(a => a.AcertoPlacar).ToList();
                apostaAgrupada = apostaAgrupada.OrderByDescending(a => a.Pontuacao).ToList();
                
                foreach (var aposta in apostaAgrupada)
                    modeloFinal.Add(new ModeloDeApostaDaLista(aposta.Id, aposta.Nome, contador++, aposta.Pontuacao, aposta.AcertoPlacar, aposta.AcertoEmpate, aposta.AcertoGanhador));
                
                modelo.Lista = modeloFinal;

                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar Apostas");
            }
        }

    }
}
