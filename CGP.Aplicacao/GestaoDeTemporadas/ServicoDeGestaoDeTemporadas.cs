using Cgp.Aplicacao.GestaoDeTemporadas.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeTemporadas
{
    public class ServicoDeGestaoDeTemporadas : IServicoDeGestaoDeTemporadas
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeTemporadas(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeTemporada RetornarTemporadasPorFiltro(ModeloDeFiltroDeTemporada filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var temporadas = this._servicoExternoDePersistencia.RepositorioDeTemporadas.RetornarTemporadasPorFiltro(filtro.Nome, filtro.Ano, filtro.Pais, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeTemporada(temporadas, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar temporadas.");
            }
        }

        public string CadastrarTemporada(ModeloDeCadastroDeTemporada modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var novaTemporada = new Temporada(modelo.Nome, modelo.Ano, modelo.Pais, usuarioBanco);
                this._servicoExternoDePersistencia.RepositorioDeTemporadas.Inserir(novaTemporada);
                this._servicoExternoDePersistencia.Persistir();

                return "Temporada incluída com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir a temporada: " + ex.InnerException);
            }
        }

        public ModeloDeEdicaoDeTemporada BuscarTemporadaPorId(int id)
        {
            try
            {
                var temporada = this._servicoExternoDePersistencia.RepositorioDeTemporadas.PegarPorId(id);
                return new ModeloDeEdicaoDeTemporada(temporada);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar times");
            }
        }

        public string AlterarDadosDaTemporada(ModeloDeEdicaoDeTemporada modelo, UsuarioLogado usuario)
        {
            var temporada = this._servicoExternoDePersistencia.RepositorioDeTemporadas.BuscarPorId(modelo.Id);
            var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

            temporada.AlterarDados(modelo.Nome, modelo.Ano, modelo.Pais, modelo.Ativo, modelo.Aberta, usuarioBanco);

            this._servicoExternoDePersistencia.Persistir();

            return "Temporada alterada com sucesso.";
        }

        public IList<Temporada> RetonarTodasAsTemporadasAtivas()
        {
            var temporadas = this._servicoExternoDePersistencia.RepositorioDeTemporadas.RetornarTodosAsTemporadasAtivas();
            return temporadas;
        }

        public string AtivarTemporada(int id, UsuarioLogado usuario)
        {
            try
            {
                var temporada = this._servicoExternoDePersistencia.RepositorioDeTemporadas.BuscarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (temporada != null)
                {
                    if (temporada.Aberta)
                        temporada.FecharTemporada(usuarioBanco);
                    else
                        temporada.AbrirTemporada(usuarioBanco);
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Temporada alterada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar a temporada: " + ex.InnerException);
            }
        }

        public int BuscarTemporadaAtiva()
        {
            var rodada = this._servicoExternoDePersistencia.RepositorioDeTemporadas.BuscarTemporadaAtiva();
            return rodada != null ? rodada.Id : 0;
        }

    }
}
