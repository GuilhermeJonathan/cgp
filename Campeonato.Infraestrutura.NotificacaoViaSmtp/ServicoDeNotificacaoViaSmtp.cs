using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using Campeonato.Infraestrutura.ServicosExternos.NotificacaoViaSmtp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.NotificacaoViaSmtp
{
    public class ServicoDeNotificacaoViaSmtp : IServicoDeNotificacao
    {
        private readonly string _servidor;
        private readonly string _senha;
        private readonly int _porta;
        private readonly string _usuario;

        public async Task EnviarNotificacao(ContatoDaNotificacao remetente,
            IEnumerable<ContatoDaNotificacao> destinatarios, string titulo, string mensagem)
        {
            if (destinatarios == null || !destinatarios.Any())
                return;

            var email = new MailMessage
            {
                From = new MailAddress(remetente.Contato, remetente.Nome),
                Subject = titulo,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Body = mensagem
            };

            email.CC.Add(new MailAddress(remetente.Contato, remetente.Nome));


            using (var cliente = new SmtpClient(this._servidor, this._porta))
            {
                cliente.Credentials = new NetworkCredential(this._usuario, this._senha);
                await cliente.SendMailAsync(email);
            }
        }
    }
}
