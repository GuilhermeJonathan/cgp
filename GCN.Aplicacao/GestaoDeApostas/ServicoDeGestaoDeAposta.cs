using Campeonato.Aplicacao.Util;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using Campeonato.Infraestrutura.ServicosExternos.ArmazenamentoEmNuvem;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ModeloDeListaDeApostas BuscarTodasApostasPorRodada(int idRodada, UsuarioLogado usuario)
        {
            try
            {
                decimal valorTotal = 0;
                var apostas = this._servicoExternoDePersistencia.RepositorioDeApostas.RetornarApostasPorRodada(idRodada);
                apostas = apostas.Where(a => a.TipoDeAposta == TipoDeAposta.Exclusiva).ToList();
                var quantidadeEncontrada = apostas.Count;

                apostas = apostas.Where(a => a.Jogos.Count > 0).OrderByDescending(a => a.Pontuacao).ToList();
                valorTotal = apostas.Sum(a => a.Valor);

                var modelo = new ModeloDeListaDeApostas(apostas, quantidadeEncontrada, valorTotal, new ModeloDeFiltroDeAposta());
                modelo.ArquivoHtml = RetornaHtmlDaLista(modelo.Lista.ToList(), modelo.NomeRodada, usuario);

                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar Apostas");
            }
        }

        public string RetornaHtmlDaLista(List<ModeloDeApostaDaLista> apostas, string nomeDaRodada, UsuarioLogado usuario)
        {
            StringBuilder html = new StringBuilder();
            var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");

            html.Append($"<html><head> <meta charset='UTF-8'></head>");
            html.Append("<style> .table{width: 100%; margin-bottom: 1rem; color: #212529; background-color: transparent;}  .table th, .table td { padding: 0.75rem; vertical-align: top; border-top: 1px solid #dee2e6;}  .table thead th { vertical-align: bottom; border-bottom: 2px solid #dee2e6;}  .table tbody + tbody { border-top: 2px solid #dee2e6; }  .text-muted { color: #6c757d !important;} .placar { font-size: 16pt; font-weight: bold; } .verde {color: #139f11 !important; font-weight: bold; } .amarelo {color: #ff6a00 !important;font-weight: bold;}");
            html.Append($"</style>");
            html.Append($"<body>");
            html.Append($"<h2>Espelho de Apostas da {nomeDaRodada}</h2>");

            foreach (var aposta in apostas)
            {
                html.Append($"<table class='table'>");
                html.Append($"<tr><th colspan='4'>{aposta.NomeUsuario} - {aposta.NomeRodada} - {aposta.TipoDaAposta}</th></tr>");
                html.Append($"<tr><th>Jogo</th><th>Times</th><th>Data</th><th>Local</th></tr>");

                foreach (var jogo in aposta.Jogos)
                {
                    html.Append($"<tr><td>");

                    html.Append($"<span class='text-muted'>{jogo.SiglaTime1}</span>&nbsp;");
                    html.Append($"<img src='{jogo.ImagemTime1}' style='max-width: 25px; margin-bottom: -10px;'/><span>");
                    html.Append($"<span class='text-muted placar {jogo.CssResultadoTime1}'> {jogo.PlacarTime1} </span>");
                    html.Append($"<span>x</span>");
                    html.Append($"<span class='text-muted placar {jogo.CssResultadoTime2}'> {jogo.PlacarTime2} </span>");
                    html.Append($"<img src='{jogo.ImagemTime2}' style='max-width: 25px; margin-bottom: -10px;'/><span>");
                    html.Append($"&nbsp;<span class='text-muted'>{jogo.SiglaTime2}</span>");
                    html.Append($"</td>");
                    html.Append($"<td>");
                    html.Append($"<span class='text-muted'>{jogo.NomeTime1}</span><br>");
                    html.Append($"<span class='text-muted'>{jogo.NomeTime2}</span>");
                    html.Append($"</td>");
                    html.Append($"<td>");
                    html.Append($"<p class='d-flex flex-column text-center'><span style='font-weight: bold;'> {jogo.DataDoJogo}</span><span> {jogo.HoraDoJogo}</span></p>");
                    html.Append($"</td>");
                    html.Append($"<td>");
                    html.Append($"<p style='text-align:center;'><span>{jogo.NomeEstadio}</span></p>");
                    html.Append($"</td>");

                    html.Append($"</tr>");
                }
            }
            html.Append($"</table></body>");
            html.Append($"<footer class='main-footer'> Emitido pelo {usuario.Nome} em {DateTime.Now.ToShortDateString()}<br> <b>Bolão</b>Brasileirão 2020 - All rights reserved.</footer>");

            html.Append($"</html>");
            return html.ToString();
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
                    aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarRodadaExclusivaPorId(idUsuario, idRodada);

                return new ModeloDeEdicaoDeAposta(aposta);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar aposta");
            }
        }

        public ModeloDeEdicaoDeAposta VisualizarApostaExclusiva(int idAposta, int idUsuario)
        {
            try
            {
                var aposta = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarRodadaExclusivaPorId(idUsuario, idAposta);
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
                var apostaExclusiva = this._servicoExternoDePersistencia.RepositorioDeApostas.PegarRodadaExclusiva(usuario.Id, id);

                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada)
                    throw new ExcecaoDeAplicacao("Rodada encontra-se finalizada.");

                if (rodada.SituacaoDaRodada == SituacaoDaRodada.Finalizada)
                {
                    if(rodada.Aberta)
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

                contador = 0;
                if (apostaExclusiva != null)
                {
                    if (apostaExclusiva.Jogos != null)
                    {
                        var idJogosExclusivos = apostaExclusiva.Jogos.Select(a => a.Id).ToArray();
                        foreach (var idJogo in idJogosExclusivos)
                        {
                            var jogo = this._servicoExternoDePersistencia.RepositorioDeJogos.PegarJogoDaApostaPorId(idJogo);
                            jogo.PlacarTime1 = placar1[contador];
                            jogo.PlacarTime2 = placar2[contador];
                            contador++;
                        }
                    }
                    apostaExclusiva.SituacaoDaAposta = SituacaoDaAposta.Salva;
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Aposta salva com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.ToString());
            }
        }

        public void SalvarArquivoDaRodada(int id, string caminho)
        {
            try
            {
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.PegarPorId(id);
               
                if (rodada != null)
                {
                    rodada.AlterarArquivo(caminho);
                }

                this._servicoExternoDePersistencia.Persistir();
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
                    if (rodada.Aberta)
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
                }

                novaAposta.SituacaoDaAposta = SituacaoDaAposta.Salva;
                usuarioBanco.SubtrairCredito($"Rodada Exclusiva {aposta.Rodada.Nome}", ValorDaAposta, usuario.Id, TipoDeSolicitacaoFinanceira.Aposta);

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
                    idUsuario = cl.First().Usuario.Id,
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
                    modeloFinal.Add(new ModeloDeApostaDaLista(aposta.Id, aposta.Nome, contador++, aposta.Pontuacao, aposta.AcertoPlacar, aposta.AcertoEmpate, aposta.AcertoGanhador, aposta.idUsuario));
                
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
