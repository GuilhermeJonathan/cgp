using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.NotificacaoViaSmtp
{
    public class ServicoDeEnvioDeEmails : IServicoDeEnvioDeEmails
    {
        public ServicoDeEnvioDeEmails()
        {

        }
        public async Task EnvioDeEmail()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress("contato@bolaobrasileirao.net.br", "Contato");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("guilhermejonathan@hotmail.com", "Guilherme Rodrigues");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
