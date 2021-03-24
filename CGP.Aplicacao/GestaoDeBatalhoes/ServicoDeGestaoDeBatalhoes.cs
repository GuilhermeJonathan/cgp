using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.GestaoDeBatalhoes.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeBatalhoes
{
    public class ServicoDeGestaoDeBatalhoes : IServicoDeGestaoDeBatalhoes
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeBatalhoes(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeBatalhoes RetonarTodosOsBatalhoes(ModeloDeFiltroDeBatalhao filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var batalhoes = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.RetornarTodosOsBatalhoes(filtro.Nome, filtro.ComandoRegional, filtro.Cidade, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeBatalhoes(batalhoes, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os batalhões");
            }
        }

        public IList<Batalhao> RetonarTodosOsBatalhoesAtivos()
        {
            var batalhoes = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.RetornarTodosOsBatalhoesAtivos();
            return batalhoes;
        }

        public IList<ModeloDeBatalhoesDaLista> RetonarTodosOsTimesParaSelect()
        {
            var batalhoes = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.RetornarTodosOsBatalhoesAtivos();
            var modelo = new List<ModeloDeBatalhoesDaLista>();
            batalhoes.ToList().ForEach(a => modelo.Add(new ModeloDeBatalhoesDaLista(a)));
            return modelo;
        }

        public ModeloDeEdicaoDeBatalhao BuscarBatalhaoPorId(int id)
        {
            try
            {
                var batalhao = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(id);
                return new ModeloDeEdicaoDeBatalhao(batalhao);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar batalhão");
            }
        }

        public string CadastrarBatalhao(ModeloDeCadastroDeBatalhao modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var comandoRegional = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.BuscarPorId(modelo.ComandoRegional);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);

                var novoBatalhao = new Batalhao(modelo.Nome, modelo.Sigla, cidade, comandoRegional, usuarioBanco);
                this._servicoExternoDePersistencia.RepositorioDeBatalhoes.Inserir(novoBatalhao);
                this._servicoExternoDePersistencia.Persistir();

                return "Batalhão incluído com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir o batalhão: " + ex.InnerException);
            }
        }

        public string AlterarDadosDoBatalhao(ModeloDeEdicaoDeBatalhao modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var comandoRegional = this._servicoExternoDePersistencia.RepositorioDeComandosRegionais.BuscarPorId(modelo.ComandoRegional);
                var batalhao = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(modelo.Id);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);

                batalhao.AlterarDados(modelo.Nome, modelo.Sigla, cidade, comandoRegional, usuarioBanco, modelo.Ativo);
                this._servicoExternoDePersistencia.Persistir();

                return "Batalhão alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o batalhão: " + ex.InnerException);
            }
        }

        public string AtivarBatalhao(int id, UsuarioLogado usuario)
        {
            try
            {
                var batalhao = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (batalhao != null)
                {
                    if (batalhao.Ativo)
                        batalhao.Inativar(usuarioBanco);
                    else
                        batalhao.Ativar(usuarioBanco);
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Batalhão alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o Batalhão: " + ex.InnerException);
            }
        }
    }
}
