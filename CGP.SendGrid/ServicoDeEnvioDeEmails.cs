using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.MontagemDeEmails;
using Cgp.Aplicacao.Util;
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
        private readonly IServicoDeMontagemDeEmails _servicoDeMontagemDeEmails;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public ServicoDeEnvioDeEmails(IServicoDeMontagemDeEmails servicoDeMontagemDeEmails, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeMontagemDeEmails = servicoDeMontagemDeEmails;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        public async Task EnvioDeEmail()
        {
            var client = new SendGridClient(VariaveisDeAmbiente.Pegar<string>("chaveSendGrid"));
            var from = new EmailAddress(VariaveisDeAmbiente.Pegar<string>("EmailDaEmpresa"), VariaveisDeAmbiente.Pegar<string>("NomeDaEmpresa"));
            var usuario = this._servicoDeGestaoDeUsuarios.BuscarSomenteUsuarioPorId(2);
            var modeloDeEmail = this._servicoDeMontagemDeEmails.MontarEmailBoasVindas(usuario);

            var subject = modeloDeEmail.Titulo;
            var to = new EmailAddress(usuario.Login.Valor, usuario.Nome.Valor);

            var plainTextContent = modeloDeEmail.Titulo;
            var htmlContent = modeloDeEmail.Mensagem;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
