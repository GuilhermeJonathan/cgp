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
        private const decimal ValorDaAposta = 20;
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
                decimal valorTotal = 0;
                var apostas = this._servicoExternoDePersistencia.RepositorioDeApostas.RetornarApostasPorFiltro(filtro.Usuario, filtro.Rodada, (int)filtro.TipoDeAposta, out quantidadeEncontrada);
                
                valorTotal = apostas.Sum(a => a.Valor);
                return new ModeloDeListaDeApostas(apostas, quantidadeEncontrada, valorTotal, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar Apostas");
            }
        }

        public ModeloDeListaDeApostas BuscarTodasApostasPorRodada(int idRodada)
        {
            try
            {
                decimal valorTotal = 0;
                var apostas = this._servicoExternoDePersistencia.RepositorioDeApostas.RetornarApostasPorRodada(idRodada);
                var quantidadeEncontrada = apostas.Count;

                apostas = apostas.Where(a => a.Jogos.Count > 0).OrderByDescending(a => a.Pontuacao).ToList();
                valorTotal = apostas.Sum(a => a.Valor);

                return new ModeloDeListaDeApostas(apostas, quantidadeEncontrada, valorTotal, new ModeloDeFiltroDeAposta());
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
                    if (usuario.PerfilDeUsuario != PerfilDeUsuario.Administrador)
                    {
                        var jogos = this._servicoExternoDePersistencia.RepositorioDeJogos.RetornarJogosPorRodada(aposta.Rodada.Id);
                        jogos.ToList().ForEach(a => aposta.Jogos.Add(new JogoDaAposta(a.DataHoraDoJogo, a.Time1, a.Time2, a.Rodada, a.Estadio, 0, 0)));
                    }
                }
            }
            else
            {
                if (usuario.PerfilDeUsuario != PerfilDeUsuario.Administrador)
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
            }

            this._servicoExternoDePersistencia.Persistir();

            var modelo = new ModeloDeEdicaoDeAposta(aposta);

            if (aposta != null)
            {
                var apostaExclusiva = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarRodadaExclusiva(aposta.Usuario.Id, aposta.Rodada.Id);
                if (apostaExclusiva != null)
                {
                    modelo.TemApostaExclusiva = true;
                    modelo.IdApostaExclusiva = apostaExclusiva.Id;
                }
            }
            return modelo;
        }

        public ModeloDeEdicaoDeAposta VisualizarAposta(int idRodada, int idUsuario)
        {
            try
            {
                var aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarPorIdRodadaEUsuario(idRodada, idUsuario);
                
                if(aposta == null)
                    aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarRodadaExclusiva(idUsuario);

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
                    aposta.SituacaoDaAposta = SituacaoDaAposta.Salva;
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Aposta salva com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.ToString());
            }
        }

        public string GerarApostaExclusiva(int id, int idRodada, int idUsuario, UsuarioLogado usuario)
        {
            try
            {
                var aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarPorId(id);
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(idRodada);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada)
                    throw new ExcecaoDeAplicacao("Rodada encontra-se finalizada.");

                if (rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada)
                {
                    if (rodada.Fechada)
                        throw new ExcecaoDeAplicacao("Rodada encontra-se fechada.");
                }

                if (usuarioBanco.Saldo  < ValorDaAposta)
                    throw new ExcecaoDeAplicacao("Você não possui créditos para realizar a aposta exclusiva.");

                if (aposta.Rodada.DataPrimeiroJogo.AddMinutes(-VariaveisDeAmbiente.Pegar<int>("TempoParaFechamentoDeRodada")) < DateTime.Now)
                    throw new ExcecaoDeAplicacao("Rodada iniciada. Não é possível alterar as apostas.");

                var novaAposta = new Aposta(usuarioBanco, rodada, TipoDeAposta.Exclusiva, ValorDaAposta);

                if (aposta != null)
                {
                    if (aposta.Jogos != null)
                    {
                        aposta.Jogos.ToList().ForEach(a => novaAposta.Jogos.Add(new JogoDaAposta(a.DataHoraDoJogo, a.Time1, a.Time2, a.Rodada, a.Estadio, a.PlacarTime1, a.PlacarTime2)));
                        this._servicoExternoDePersistencia.RepositorioDeApostas.Inserir(novaAposta);
                    }
                    aposta.SituacaoDaAposta = SituacaoDaAposta.Salva;
                }

                usuarioBanco.SubtrairCredito($"Rodada Exclusiva {aposta.Rodada.Nome}", ValorDaAposta, usuario.Id);

                this._servicoExternoDePersistencia.Persistir();

                return "Aposta gerada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public ModeloDeListaDeApostas BuscarResultado(int idRodada, TipoDeAposta tipoDeAposta)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var apostas = this._servicoExternoDePersistencia.RepositorioDeApostas.RetornarApostasParaResultado(idRodada, tipoDeAposta);

                apostas = apostas.Where(a => a.Jogos.Count > 0).ToList();

                var modelo = new ModeloDeListaDeApostas(apostas, quantidadeEncontrada, 0, new ModeloDeFiltroDeAposta());

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
