using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.GestaoDeEstadio.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeEstadio
{
    public class ServicoDeGestaoDeEstadios : IServicoDeGestaoDeEstadios
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeEstadios(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeEstadios RetonarTodosOsEstadios(ModeloDeFiltroDeEstadio filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var estadios = this._servicoExternoDePersistencia.RepositorioDeEstadios.RetornarTodosOsEstadios(filtro.Nome, filtro.Time, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeEstadios(estadios, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar estádios");
            }
        }

        public IList<Estadio> RetonarTodosOsEstadiosAtivos()
        {
            var estadios = this._servicoExternoDePersistencia.RepositorioDeEstadios.RetornarTodosOsEstadiosAtivos();
            return estadios;
        }

        public ModeloDeEdicaoDeEstadio BuscarEstadioPorId(int id)
        {
            try
            {
                var estadio = this._servicoExternoDePersistencia.RepositorioDeEstadios.PegarPorId(id);
                return new ModeloDeEdicaoDeEstadio(estadio);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar times");
            }
        }

        public string CadastrarEstadio(ModeloDeCadastroDeEstadio modelo, UsuarioLogado usuario)
        {
            try
            {
                var time = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(modelo.Time);
                var novoEstadio = new Estadio(modelo.Nome, modelo.Cidade, time);
                this._servicoExternoDePersistencia.RepositorioDeEstadios.Inserir(novoEstadio);
                this._servicoExternoDePersistencia.Persistir();

                return "Estádio incluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir o estádio: " + ex.InnerException);
            }
        }

        public string AlterarDadosDoEstadio(ModeloDeEdicaoDeEstadio modelo, UsuarioLogado usuario)
        {
            try
            {
                var estadio = this._servicoExternoDePersistencia.RepositorioDeEstadios.PegarPorId(modelo.Id);
                var time = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(modelo.Time);
                estadio.AlterarDados(modelo.Nome, modelo.Cidade, time, modelo.Ativo);
                this._servicoExternoDePersistencia.Persistir();

                return "Estádio alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o estádio: " + ex.InnerException);
            }
        }
    }
}
