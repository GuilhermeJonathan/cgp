using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.SendGrid
{
    public interface IServicoDeEnvioDeEmails
    {
        Task EnvioDeEmail();
    }
}
