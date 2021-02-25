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

                usuarioParaAlterar.AlterarMeusDados(modelo.Nome, modelo.Email, modelo.Ddd, modelo.Telefone, modelo.TipoDePix, modelo.ChavePix);
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

        public string CadastrarSaldo(int id, decimal saldo, UsuarioLogado usuario, string tipoDeSolicitacaoFinanceira)
        {
            try
            {
                if (saldo <= 0)
                    throw new ExcecaoDeAplicacao("Não é possível adicionar saldo negativo à conta");

                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (usuarioParaAlterar != null)
                {
                    var TipoFinanceiro = tipoDeSolicitacaoFinanceira == "Dinheiro "? TipoDeSolicitacaoFinanceira.Dinheiro : tipoDeSolicitacaoFinanceira == "Premiacao" ? TipoDeSolicitacaoFinanceira.Premiacao : TipoDeSolicitacaoFinanceira.Dinheiro;
                    if (saldo > 0)
                        usuarioParaAlterar.AdicionarSaldo($"Adição de Saldo", saldo, usuarioBanco.Id, TipoFinanceiro);   
                }

                this._servicoExternoDePersistencia.Persistir();
                return "Saldo adicionado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }

        public string CadastrarSaldoParaPremiacao(Usuario usuario, decimal saldo, UsuarioLogado usuarioLogado, string textoPremiacao)
        {
            try
            {
                if (saldo <= 0)
                    throw new ExcecaoDeAplicacao("Não é possível adicionar saldo negativo à conta");

                if (usuario != null)
                {
                    if (saldo > 0)
                        usuario.AdicionarSaldo(textoPremiacao, saldo, usuarioLogado.Id, TipoDeSolicitacaoFinanceira.Premiacao);
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

        public ModeloDeEdicaoDeUsuario BuscarMeusDados(UsuarioLogado usuarioLogado)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuarioLogado.Id);
            return new ModeloDeEdicaoDeUsuario(usuario);
        }

        public ModeloDeEdicaoDeUsuario BuscarUsuarioComHistoricoPorId(int id)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarUsuarioComHistorico(id);
            usuario.HistoricosFinanceiros = usuario.HistoricosFinanceiros.OrderByDescending(a => a.DataDoCadastro).ToList();

            return new ModeloDeEdicaoDeUsuario(usuario);
        }

        public string RetirarSaldo(decimal saldo, UsuarioLogado usuario, int tipoDePix, string chavePix)
        {
            try
            {
                if (saldo <= 0)
                    throw new ExcecaoDeAplicacao("Não é possível retirar saldo negativo da conta");

                var usuarioParaAlterar = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (saldo > usuarioParaAlterar.Saldo)
                    throw new ExcecaoDeAplicacao("O pedido de retirada é maior que o saldo da conta.");

                if (usuarioParaAlterar != null)
                {
                    if (saldo > 0)
                    {
                        if(String.IsNullOrEmpty(usuarioParaAlterar.ChavePix))
                            usuarioParaAlterar.CadastrarPix((TipoDePix)tipoDePix, chavePix);

                        usuarioParaAlterar.SubtrairCreditoPix($"Pedido de Retirada", saldo, usuarioParaAlterar.Id, TipoDeSolicitacaoFinanceira.Saque, (TipoDePix)tipoDePix, chavePix);
                    }
                }

                this._servicoExternoDePersistencia.Persistir();
                return "Pedido de retirada realizado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao(ex.Message);
            }
        }
    }
}
