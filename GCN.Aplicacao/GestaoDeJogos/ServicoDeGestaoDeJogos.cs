using Campeonato.Aplicacao.Comum;
using Campeonato.Aplicacao.GestaoDeJogos.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeJogos
{
    public class ServicoDeGestaoDeJogos : IServicoDeGestaoDeJogos
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeJogos(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeJogos RetonarTodosOsJogos(ModeloDeFiltroDeJogo filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var dataParaFiltro = new DateTime();

                if (!String.IsNullOrEmpty(filtro.DataHoraDoJogo))
                    dataParaFiltro = DateTime.Parse($"{filtro.DataHoraDoJogo} 00:00:00");
                
                var quantidadeEncontrada = 0;
                var jogos = this._servicoExternoDePersistencia.RepositorioDeJogos.RetornarJogosPorFiltro(filtro.Time, filtro.Rodada, dataParaFiltro, out quantidadeEncontrada);

                return new ModeloDeListaDeJogos(jogos, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar estádios");
            }
        }

        public string CadastrarJogo(ModeloDeCadastroDeJogo modelo, UsuarioLogado usuario)
        {
            try
            {
                var dataHoraDoJogo = new DateTime();

                if(!String.IsNullOrEmpty(modelo.DataDoJogo) && !String.IsNullOrEmpty(modelo.HoraDoJogo))
                {
                    dataHoraDoJogo = DateTime.Parse($"{modelo.DataDoJogo} {modelo.HoraDoJogo}");
                }

                if(modelo.Estadio == 0)
                    throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente um estádio.");

                if (modelo.Time1 == 0)
                    throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente um time da casa ");

                if (modelo.Time2 == 0)
                    throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente um time visitante");

                if (modelo.Time1 == modelo.Time2)
                    throw new ExcecaoDeAplicacao("Os times devem ser diferentes");

                var estadio = this._servicoExternoDePersistencia.RepositorioDeEstadios.BuscarPorId(modelo.Estadio);
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.BuscarPorId(modelo.Rodada);
                var time1 = this._servicoExternoDePersistencia.RepositorioDeTimes.BuscarPorId(modelo.Time1);
                var time2 = this._servicoExternoDePersistencia.RepositorioDeTimes.BuscarPorId(modelo.Time2);

                var novoJogo = new Jogo(dataHoraDoJogo, estadio, time1, time2, rodada);

      
                this._servicoExternoDePersistencia.RepositorioDeJogos.Inserir(novoJogo);
                this._servicoExternoDePersistencia.Persistir();

                return "Jogo cadastrado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir jogo: " + ex.InnerException);
            }
        }

        public ModeloDeEdicaoDeJogo BuscarJogoPorId(int id)
        {
            try
            {
                var jogo = this._servicoExternoDePersistencia.RepositorioDeJogos.PegarPorId(id);
                return new ModeloDeEdicaoDeJogo(jogo);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar times");
            }
        }

        public string AlterarDadosDoJogo(ModeloDeEdicaoDeJogo modelo, UsuarioLogado usuario)
        {
            try
            {
                var dataHoraDoJogo = new DateTime();

                if (!String.IsNullOrEmpty(modelo.DataDoJogo) && !String.IsNullOrEmpty(modelo.HoraDoJogo))
                {
                    dataHoraDoJogo = DateTime.Parse($"{modelo.DataDoJogo} {modelo.HoraDoJogo}");

                    if (dataHoraDoJogo == null)
                        throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente uma data e hora do jogo.");
                }

                if (modelo.Estadio == 0)
                    throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente um estádio.");

                if (modelo.Time1 == 0)
                    throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente um time da casa ");

                if (modelo.Time2 == 0)
                    throw new ExcecaoDeAplicacao("Deve incluir obrigatoriamente um time visitante");

                if (modelo.Time1 == modelo.Time2)
                    throw new ExcecaoDeAplicacao("Os times devem ser diferentes");

                var jogo = this._servicoExternoDePersistencia.RepositorioDeJogos.BuscarPorId(modelo.Id);

                var estadio = this._servicoExternoDePersistencia.RepositorioDeEstadios.BuscarPorId(modelo.Estadio);
                var rodada = this._servicoExternoDePersistencia.RepositorioDeRodadas.BuscarPorId(modelo.Rodada);
                var time1 = this._servicoExternoDePersistencia.RepositorioDeTimes.BuscarPorId(modelo.Time1);
                var time2 = this._servicoExternoDePersistencia.RepositorioDeTimes.BuscarPorId(modelo.Time2);

                jogo.AlterarDadosDoJogo(dataHoraDoJogo, estadio, time1, time2, rodada);

                this._servicoExternoDePersistencia.Persistir();

                return "Jogo alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o jogo: " + ex.InnerException);
            }

        }

    }
}
