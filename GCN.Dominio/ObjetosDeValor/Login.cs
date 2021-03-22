using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class LoginUsuario
    {
        private LoginUsuario()
        {
        }

        public LoginUsuario(string valor)
        {
            var email = new Email.Email(valor);
            this.Valor = email.Endereco;
        }

        public string Valor { get; private set; }
    }
}
