using Campeonato.Aplicacao.Util;
using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.MontagemDeEmails
{
    public class ServicoDeMontagemDeEmails : IServicoDeMontagemDeEmails
    {
        public ServicoDeMontagemDeEmails()
        {

        }

        public ModeloDeEmail MontarEmailBoasVindas(Usuario usuario)
        {
            var titulo = $"Bem Vindo(a) ao Bolão Brasileirão";
            string caminho = "arquivos/email/emailPadrao.html";
            string corpo = EfetuarODownloadDoTemplateDeEmail(caminho);
            var mensagem = new StringBuilder();

            mensagem.Append($"<p>Olá <b>{ usuario.Nome.Valor }</b>, seu cadastro no {VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa")} foi concluído com sucesso!</p>");            
            corpo = corpo.Replace("{TITULO}", titulo);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());

            var link = "bolaobrasileirao.net.br";

            corpo = corpo.Replace("{URL}", link);
            corpo = corpo.Replace("{MENSAGEM}", mensagem.ToString());
            corpo = corpo.Replace("{DESCRICAO_BOTAO}", "Acesse Agora");

            return new ModeloDeEmail(titulo, corpo.ToString());
        }

        private string EfetuarODownloadDoTemplateDeEmail(string caminho)
        {
            string url = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");
            string urlDoArquivo = url + caminho;
            string urlLogoEmpresa = url + "arquivos/logo/bolaoBrasileirao.png";

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
