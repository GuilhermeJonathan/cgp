using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.NotificacaoViaSmtp
{
    public interface IServicoDeEnvioDeEmails
    {
        Task EnvioDeEmail();
    }
}
