using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios
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
            
            var batalhao = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(modelo.Batalhao);

            var senha = new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash);
            var novologin = new LoginUsuario(modelo.Email);

            var novoUsuario = new Usuario(new Nome(modelo.Nome), novologin, senha, batalhao, modelo.Matricula);

            this._servicoExternoDePersistencia.RepositorioDeUsuarios.Inserir(novoUsuario);
            this._servicoExternoDePersistencia.Persistir();

            return "Usuário cadastrado com sucesso.";
        }

        public string AlterarDadosDoUsuario(ModeloDeEdicaoDeUsuario modelo, UsuarioLogado usuario)
        {
            try
            {
                var batalhao = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(modelo.Batalhao);
                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(modelo.Id);
                usuarioParaAlterar.AlterarDados(modelo.Nome, modelo.Email, modelo.Ativo, modelo.PerfilDeUsuario, batalhao, modelo.Matricula);
                this._servicoExternoDePersistencia.Persistir();

                return "Usuário alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public string EditarMeusDados(ModeloDeEdicaoDeUsuario modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (usuarioParaAlterar.Login.Valor != modelo.Email)
                {
                    var usuarioComMesmoLogin = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarAtivoPorLogin(modelo.Email);

                    if (usuarioComMesmoLogin != null)
                        throw new ExcecaoDeAplicacao("Já existe um usuário com o mesmo login");
                }
                var batalhao = this._servicoExternoDePersistencia.RepositorioDeBatalhoes.PegarPorId(modelo.Batalhao);

                usuarioParaAlterar.AlterarMeusDados(modelo.Nome, modelo.Email, modelo.Ddd, modelo.Telefone, batalhao);
                this._servicoExternoDePersistencia.Persistir();

                return "Meus dados alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public string AlterarSenha(ModeloDeEdicaoDeUsuario modelo)
        {   
            if(modelo.Senha != modelo.RepetirSenha)
                throw new ExcecaoDeAplicacao("As senhas são diferentes.");

            var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(modelo.Id);
            var senha = new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash);
            usuarioParaAlterar.AlterarSenha(senha);
            this._servicoExternoDePersistencia.Persistir();
            return "Senha alterada com sucesso.";
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
            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.RetornarUsuariosPorFiltro(filtro.Nome, filtro.Email, filtro.Batalhao, filtro.Ativo, 
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

                return "Usuário alterado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar o usuário: " + ex.InnerException);
            }
        }

        public int BuscarUsuariosNovos()
        {
            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarQtdUsuariosNovos();
            return usuarios;
        }

        public Usuario BuscarSomenteUsuarioPorId(int id)
        {
            return this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(id);
        }

        public ModeloDeEdicaoDeUsuario BuscarUsuarioPorId(int id)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarUsuarioCompletoPorId(id);
            return new ModeloDeEdicaoDeUsuario(usuario);
        }

        public ModeloDeEdicaoDeUsuario BuscarMeusDados(UsuarioLogado usuarioLogado)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarUsuarioCompletoPorId(usuarioLogado.Id);
            return new ModeloDeEdicaoDeUsuario(usuario);
        }
    }
}
