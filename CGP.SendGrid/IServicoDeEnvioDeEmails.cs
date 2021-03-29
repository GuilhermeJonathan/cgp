using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.SendGrid
{
    public interface IServicoDeEnvioDeEmails
    {
        Task EnvioDeEmail(Usuario usuario, string titulo, string mensagem);
    }
}
