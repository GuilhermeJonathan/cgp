using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.MontagemDeEmails
{
    public class ServicoDeMontagemDeEmails : IServicoDeMontagemDeEmails
    {
        public ServicoDeMontagemDeEmails()
        {

        }

        public ModeloDeEmail MontarEmailBoasVindas(Usuario usuario)
        {
            var titulo = $"Bem Vindo(a) ao Caráter Geral Policial";
            string caminho = "arquivos/email/emailPadrao.html";
            string corpo = EfetuarODownloadDoTemplateDeEmail(caminho);
            var mensagem = new StringBuilder();

            mensagem.Append($"<p>Olá <b>{ usuario.Nome.Valor }</b>, seu cadastro no {VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa")} foi concluído com sucesso!</p>");            
            mensagem.Append($"<p>Você receberá um email quando o Adminsitrador aprovar seu cadastro.</p>");
            corpo = corpo.Replace("{TITULO}", titulo);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());

            var link = $"{VariaveisDeAmbiente.Pegar<string>("NomeDoSite")}";

            corpo = corpo.Replace("{URL}", link);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());
            corpo = corpo.Replace("{DESCRICAO_BOTAO}", "Acesse Agora");

            return new ModeloDeEmail(titulo, corpo.ToString());
        }

        public ModeloDeEmail MontarEmailCadastroAtivo(Usuario usuario)
        {
            var titulo = $"Seu usuário foi ativado no {VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa")}";
            string caminho = "arquivos/email/emailPadrao.html";
            string corpo = EfetuarODownloadDoTemplateDeEmail(caminho);
            var mensagem = new StringBuilder();

            mensagem.Append($"<p>Olá <b>{ usuario.Nome.Valor }</b>, seu cadastro no {VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa")} foi ativado com sucesso!</p>");
            mensagem.Append($"<p>Para isto, basta acessar o sistema e realizar o login com seus dados.</p>");
            corpo = corpo.Replace("{TITULO}", titulo);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());

            var link = $"{VariaveisDeAmbiente.Pegar<string>("NomeDoSite")}";

            corpo = corpo.Replace("{URL}", link);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());
            corpo = corpo.Replace("{DESCRICAO_BOTAO}", "Acesse Agora");

            return new ModeloDeEmail(titulo, corpo.ToString());
        }

        public ModeloDeEmail MontarEmailRenovacaoSenha(Usuario usuario, string token)
        {
            var titulo = $"Renovação de Senha - {VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa")}";
            string caminho = "arquivos/email/emailPadrao.html";
            string corpo = EfetuarODownloadDoTemplateDeEmail(caminho);
            var mensagem = new StringBuilder();

            mensagem.Append($"<p>Olá <b>{ usuario.Nome.Valor }</b>, você solicitou renovação de senha no {VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa")}.</p>");
            mensagem.Append($"<p>Para isto, basta clicar no link abaixo para renovar sua senha.</p>");
            corpo = corpo.Replace("{TITULO}", titulo);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());

            var link = $"{VariaveisDeAmbiente.Pegar<string>("NomeDoSite")}/EsqueciMinhaSenha/RenovacaoDeSenha?t={token}";

            corpo = corpo.Replace("{URL}", link);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());
            corpo = corpo.Replace("{DESCRICAO_BOTAO}", "Clique para renovar senha");

            return new ModeloDeEmail(titulo, corpo.ToString());
        }

        private string EfetuarODownloadDoTemplateDeEmail(string caminho)
        {
            string url = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");
            string urlDoArquivo = url + caminho;
            string urlLogoEmpresa = url + "arquivos/logo/logoPmdf.png";

            using (WebClient client = new WebClient())
            {
                string body = client.DownloadString(urlDoArquivo);

                string data = DateTime.Now.ToString("U");
                body = body.Replace("{DATA_DE_ENVIO}", data);
                body = body.Replace("{BLOB_AZURE}", url);
                body = body.Replace("{NUMERO_ALEATORIO}", new Random().Next().ToString());
                body = body.Replace("{NOME_DA_EMPRESA}", VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa"));
                body = body.Replace("{LOGO_EMPRESA}", urlLogoEmpresa);
                return body;
            }
        }
    }
}
