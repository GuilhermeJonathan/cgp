using Cgp.Aplicacao.GestaoDeComandosRegionais.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais
{
    public class ServicoDeGestaoDeComandosRegionais : IServicoDeGestaoDeComandosRegionais
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeComandosRegionais(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public IList<ComandoRegional> RetonarTodosOsComandosRegionaisAtivos()
        {
            var comandos = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.RetornarTodosOsComandosRegionaisAtivos();
            return comandos;
        }

        public ModeloDeListaDeComandosRegionais RetonarComandosRegionaisPorFiltro(ModeloDeFiltroDeComandoRegional filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var comandos = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.RetornarTodosOsComandosRegionaisPorFiltro(filtro.Nome, filtro.Ativo, out quantidadeEncontrada);
                return new ModeloDeListaDeComandosRegionais(comandos, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os comandos regionais");
            }
        }

        public string CadastrarComandoRegional(ModeloDeCadastroDeComandoRegional modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var novoComando = new ComandoRegional(modelo.Nome, modelo.Sigla, usuarioBanco);
                this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.Inserir(novoComando);
                this._servicoExternoDePersistencia.Persistir();

                return "Comando Regional incluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir o Comando Regional: " + ex.InnerException);
            }
        }

        public ModeloDeEdicaoDeComandoRegional BuscarComandoRegionalPorId(int id)
        {
            try
            {
                var comando = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.PegarPorId(id);
                return new ModeloDeEdicaoDeComandoRegional(comando);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar Comando Regional");
            }
        }

        public string AlterarDadosDoComandoRegional(ModeloDeEdicaoDeComandoRegional modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var comandoRegional = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.BuscarPorId(modelo.Id);
                comandoRegional.AlterarDados(modelo.Nome, modelo.Sigla, usuarioBanco, modelo.Ativo);
                this._servicoExternoDePersistencia.Persistir();

                return "Comando Regional alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Comando Regional: " + ex.InnerException);
            }
        }

        public string AtivarComando(int id, UsuarioLogado usuario)
        {
            try
            {
                var comandoRegional = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.BuscarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (comandoRegional != null)
                {
                    if (comandoRegional.Ativo)
                        comandoRegional.Inativar(usuarioBanco);
                    else
                        comandoRegional.Ativar(usuarioBanco);
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Comando Regional alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Comando Regional: " + ex.InnerException);
            }
        }
    }
}
