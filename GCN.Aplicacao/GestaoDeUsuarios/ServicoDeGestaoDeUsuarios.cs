using Campeonato.Aplicacao.Comum;
using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios
{
    public class ServicoDeGestaoDeUsuarios : IServicoDeGestaoDeUsuarios
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;

        public ServicoDeGestaoDeUsuarios(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, 
            IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
        }

        public string CadastrarNovoUsuario(ModeloDeCadastroDeUsuario modelo)
        {
            var usuarioComMesmoLogin = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarAtivoPorLogin(modelo.Email);

            if (usuarioComMesmoLogin != null)
                throw new ExcecaoDeAplicacao("Já existe um usuário com o mesmo login");

            var senha = new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash);
            var novologin = new LoginUsuario(modelo.Email);

            var novoUsuario = new Usuario(new Nome(modelo.Nome), novologin, senha);

            this._servicoExternoDePersistencia.RepositorioDeUsuarios.Inserir(novoUsuario);
            this._servicoExternoDePersistencia.Persistir();

            return "Usuário cadastrado com sucesso.";
        }

        public string AlterarDadosDoUsuario(ModeloDeEdicaoDeUsuario modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(modelo.Id);
                usuarioParaAlterar.AlterarDados(modelo.Nome, modelo.Email, modelo.Ativo, modelo.PerfilDeUsuario);
                this._servicoExternoDePersistencia.Persistir();

                return "Usuário alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public IList<ModeloDeUsuarioDaLista> RetonarTodosOsUsuariosAtivos()
        {
            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.RetornarTodosUsuarios();
            var modelo = new List<ModeloDeUsuarioDaLista>();

            usuarios.ToList().ForEach(a => modelo.Add(new ModeloDeUsuarioDaLista(a)));
            return modelo;
        }

        public ModeloDeListaDeUsuarios RetonarUsuariosPorFiltro(ModeloDeFiltroDeUsuario filtro, int pagina, int registrosPorPagina = 30)
        {
            var quantidadeEncontrada = 0;
            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.RetornarUsuariosPorFiltro(filtro.Nome, filtro.Email, filtro.Ativo, 
                pagina, registrosPorPagina, out quantidadeEncontrada);

            return new ModeloDeListaDeUsuarios(usuarios, quantidadeEncontrada, filtro);
        }
        
        public string AtivarUsuario(int id, UsuarioLogado usuario)
        {
            try
            {
                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (usuarioParaAlterar != null)
                {
                    if (usuarioParaAlterar.Ativo)
                        usuarioParaAlterar.InativarUsuario();
                    else
                        usuarioParaAlterar.AtivarUsuario();
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Ususário alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar a rodada: " + ex.InnerException);
            }
        }

        public string CadastrarSaldo(int id, decimal saldo, UsuarioLogado usuario)
        {
            try
            {
                if (saldo <= 0)
                    throw new ExcecaoDeAplicacao("Não é possível adicionar saldo negativo à conta");

                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (usuarioParaAlterar != null)
                {
                    if(saldo > 0)
                        usuarioParaAlterar.AdicionarSaldo($"Adição de Saldo", saldo, usuarioBanco.Id);   
                }

                this._servicoExternoDePersistencia.Persistir();
                return "Saldo adicionado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public int BuscarUsuariosNovos()
        {
            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarQtdUsuariosNovos();
            return usuarios;
        }

        public ModeloDeEdicaoDeUsuario BuscarUsuarioPorId(int id)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(id);
            return new ModeloDeEdicaoDeUsuario(usuario);
        }

        public ModeloDeEdicaoDeUsuario BuscarUsuarioComHistoricoPorId(int id)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarUsuarioComHistorico(id);
            usuario.HistoricosFinanceiros = usuario.HistoricosFinanceiros.OrderBy(a => a.DataDoCadastro).ToList();

            return new ModeloDeEdicaoDeUsuario(usuario);
        }
    }
}
