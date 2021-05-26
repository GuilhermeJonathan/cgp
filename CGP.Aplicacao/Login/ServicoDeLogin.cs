using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.Criptografia;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Login.Modelos;
using Cgp.Aplicacao.MontagemDeEmails;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.BuscaViaDapper;
using Cgp.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using Cgp.SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly IServicoDeLoginAd _servicoDeLoginAd;
        private readonly IServicoDeGestaoDeUsuariosSGPOL _servicoDeGestaoDeUsuariosSGPOL;

        public ServicoDeLogin(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoExternoDeAutenticacao servicoDeAutenticacao,
            IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha, IServicoDeEnvioDeEmails servicoDeEnvioDeEmails, IServicoDeMontagemDeEmails servicoDeMontagemDeEmails,
            IServicoDeCriptografia servicoDeCriptografia, IServicoDeLoginAd servicoDeLoginAd, IServicoDeGestaoDeUsuariosSGPOL servicoDeGestaoDeUsuariosSGPOL)
        {
            _servicoExternoDePersistencia = servicoExternoDePersistencia;
            _servicoDeAutenticacao = servicoDeAutenticacao;
            _servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
            this._servicoDeEnvioDeEmails = servicoDeEnvioDeEmails;
            this._servicoDeMontagemDeEmails = servicoDeMontagemDeEmails;
            this._servicoDeCriptografia = servicoDeCriptografia;
            this._servicoDeLoginAd = servicoDeLoginAd;
            this._servicoDeGestaoDeUsuariosSGPOL = servicoDeGestaoDeUsuariosSGPOL;
        }

        public async Task EntrarAsync(ModeloDeLogin modelo)
        {
            var usuario = new Usuario();
            var loginAd = this._servicoDeLoginAd.Autenticar(modelo.Login.ToUpper(), modelo.Senha);

            if(loginAd == null)
                usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarPorLoginESenha(modelo.Login, modelo.SenhaCriptograda(this._servicoDeGeracaoDeHashSha.GerarHash));

            //LoginAD Success
            if (loginAd != null)
            {
                usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarPorMatricula(ValidaMatricula(loginAd.Matricula));
              
                if (usuario == null)
                {
                    var usuarioSGPOL = await this._servicoDeGestaoDeUsuariosSGPOL.BuscarDadosUsuario(modelo.Login, modelo.SenhaCriptograda(this._servicoDeGeracaoDeHashSha.EncodeToBase64));
                    var cpf = Regex.Replace(usuarioSGPOL.cpf, "[^0-9a-zA-Z]+", "");

                    var nome = loginAd.Nome.Valor.Split(',')[0].Split(' ').ToList();
                    nome.RemoveAt(0);
                    var nomeCompleto = nome.Aggregate((partialPhrase, word) => $"{partialPhrase} {word}");

                    var senha = new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash);
                    var novoUsuario = new Usuario(new Nome(nomeCompleto), senha, loginAd.Matricula);
                    
                    if(usuarioSGPOL != null)
                        novoUsuario.AlterarDadosDoSgpol(new Nome(usuarioSGPOL.nome), senha, usuarioSGPOL.matricula, cpf, usuarioSGPOL.nomeGuerra, usuarioSGPOL.posto, usuarioSGPOL.lotacao, usuarioSGPOL.lotacaoCodigo, usuarioSGPOL.celular);
                    
                    this._servicoExternoDePersistencia.RepositorioDeUsuarios.Inserir(novoUsuario);
                    try
                    {
                        this._servicoExternoDePersistencia.Persistir();
                    } catch(Exception ex)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                    usuario = novoUsuario;
                }

                if (usuario != null && !usuario.ValidadoGenesis)
                {
                    var usuarioSGPOL = await this._servicoDeGestaoDeUsuariosSGPOL.BuscarDadosUsuario(modelo.Login, modelo.SenhaCriptograda(this._servicoDeGeracaoDeHashSha.EncodeToBase64));
                    var cpf = Regex.Replace(usuarioSGPOL.cpf, "[^0-9a-zA-Z]+", "");
                    var senha = new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash);

                    usuario.AlterarDadosDoSgpol(new Nome(usuarioSGPOL.nome), senha, usuarioSGPOL.matricula, cpf, usuarioSGPOL.nomeGuerra, usuarioSGPOL.posto, usuarioSGPOL.lotacao, usuarioSGPOL.lotacaoCodigo, usuarioSGPOL.celular);
                }

                this._servicoExternoDePersistencia.Persistir();
            }
            else
                usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarPorLoginESenha(modelo.Login, modelo.SenhaCriptograda(this._servicoDeGeracaoDeHashSha.GerarHash));

            if(usuario == null)
                throw new ExcecaoDeAplicacao("Usuário e/ou senha inválidos");

            if (!usuario.Ativo)
                throw new ExcecaoDeAplicacao("Usuário inativo. Ainda não validado pelo Administrador.");

            var dadosDaSessao = new Dictionary<string, object>
                {
                    { "id", usuario.Id },
                    { "nome", usuario.Nome.Valor.Contains(' ') ? usuario.Nome.Valor.Split(' ')[0] : usuario.Nome.Valor},
                    { "nomeCompleto", usuario.Nome.Valor },
                    { "cpf", !String.IsNullOrEmpty(usuario.Cpf) ? usuario.Cpf : string.Empty},
                    { "matricula", !String.IsNullOrEmpty(usuario.Matricula) ? usuario.Matricula : string.Empty},
                    { "login", usuario.Login.Valor },
                    { "perfil", usuario.PerfilDeUsuario },
                    { "lotacao", usuario.Lotacao },
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

        public string ValidaMatricula(string usuario)
        {
            if (usuario.Length == 4)
                usuario = $"0000{usuario}";
            else if (usuario.Length == 5)
                usuario = $"000{usuario}";
            else if (usuario.Length == 6)
                usuario = $"00{usuario}";
            else if (usuario.Length == 7)
                usuario = $"0{usuario}";
            else if (usuario.Length == 8)
                usuario = usuario.Substring(1, usuario.Length - 1);

            return usuario.Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}
