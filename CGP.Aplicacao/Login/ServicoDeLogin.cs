using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.Criptografia;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Login.Modelos;
using Cgp.Aplicacao.MontagemDeEmails;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using Cgp.SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Login
{
    public class ServicoDeLogin : IServicoDeLogin
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoExternoDeAutenticacao _servicoDeAutenticacao;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;
        private readonly IServicoDeMontagemDeEmails _servicoDeMontagemDeEmails;
        private readonly IServicoDeEnvioDeEmails _servicoDeEnvioDeEmails;
        private readonly IServicoDeCriptografia _servicoDeCriptografia;


        public ServicoDeLogin(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoExternoDeAutenticacao servicoDeAutenticacao,
            IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha, IServicoDeEnvioDeEmails servicoDeEnvioDeEmails, IServicoDeMontagemDeEmails servicoDeMontagemDeEmails,
            IServicoDeCriptografia servicoDeCriptografia)
        {
            _servicoExternoDePersistencia = servicoExternoDePersistencia;
            _servicoDeAutenticacao = servicoDeAutenticacao;
            _servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
            this._servicoDeEnvioDeEmails = servicoDeEnvioDeEmails;
            this._servicoDeMontagemDeEmails = servicoDeMontagemDeEmails;
            this._servicoDeCriptografia = servicoDeCriptografia;
        }

        public void Entrar(ModeloDeLogin modelo)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarPorLoginESenha(modelo.Login, modelo.SenhaCriptograda(this._servicoDeGeracaoDeHashSha.GerarHash));

            if(usuario == null)
                throw new ExcecaoDeAplicacao("Usuário e/ou senha inválidos");

            if (!usuario.Ativo)
                throw new ExcecaoDeAplicacao("Usuário inativo. Ainda não validado pelo Administrador.");

            var dadosDaSessao = new Dictionary<string, object>
                {
                    { "id", usuario.Id },
                    { "nome", usuario.Nome.Valor },
                    { "login", usuario.Login.Valor },
                    { "perfil", usuario.PerfilDeUsuario },
                    { "dataDoCadastro", usuario.DataDoCadastro }
                };

            this._servicoDeAutenticacao.Acessar(dadosDaSessao);
        }

        public async Task<string> EnviarEmailEsqueciMinhaSenha(string login)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarAtivoPorLogin(login);

            if (usuario == null)
                throw new ExcecaoDeAplicacao("Usuário não encontrado.");

            if (!usuario.Ativo)
                throw new ExcecaoDeAplicacao("Usuário inativado. Mande email para contato@carageral.com.br solicitando ativação.");

            var token = this._servicoDeCriptografia.Encriptar($"{usuario.Login.Valor}#{usuario.Id}");

            usuario.IncluirToken(token);
            var modeloDeEmail = this._servicoDeMontagemDeEmails.MontarEmailRenovacaoSenha(usuario, token);

            await this._servicoDeEnvioDeEmails.EnvioDeEmail(usuario, modeloDeEmail.Titulo, modeloDeEmail.Mensagem);

            this._servicoExternoDePersistencia.Persistir();
            return "Foi encaminhado um email contendo as instruções para renovação de senha.";
        }

        public ModeloDeEdicaoDeUsuario ValidarTokenRetornarUsuario(string token)
        {
            var tokenDescriptografado = this._servicoDeCriptografia.Decriptar(token);

            var tokenDividido = tokenDescriptografado.Split(new char[] { '#', '#' }, StringSplitOptions.RemoveEmptyEntries);
            if(tokenDividido.Length != 2)
                throw new ExcecaoDeAplicacao("Token inválido");

            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarAtivoPorLogin(tokenDividido[0]);

            if (usuario == null)
            {
                return new ModeloDeEdicaoDeUsuario();
                throw new ExcecaoDeAplicacao("Usuário não encontrado.");
            }

            var modelo = new ModeloDeEdicaoDeUsuario(usuario);

            return modelo;
        }

        public void Sair()
        {
            this._servicoDeAutenticacao.Sair();
        }
    }
}
