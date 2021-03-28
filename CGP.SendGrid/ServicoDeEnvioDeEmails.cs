using Cgp.Dominio.Entidades;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.SendGrid
{
    public class ServicoDeEnvioDeEmails : IServicoDeEnvioDeEmails
    {
        private readonly string _chave;
        private readonly string _emailEmpresa;
        private readonly string _nomeEmpresa;

        public ServicoDeEnvioDeEmails(string chave, string email, string empresa)
        {
            this._chave = chave;
            this._emailEmpresa = email;
            this._nomeEmpresa = empresa;
        }

        public async Task EnvioDeEmailBoasVindas(Usuario usuario, string titulo, string mensagem)
        {
            var client = new SendGridClient(_chave);
            var from = new EmailAddress(_emailEmpresa, _nomeEmpresa);
            
            var subject = titulo;
            var to = new EmailAddress(usuario.Login.Valor, usuario.Nome.Valor);

            var plainTextContent = titulo;
            var htmlContent = mensagem;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
