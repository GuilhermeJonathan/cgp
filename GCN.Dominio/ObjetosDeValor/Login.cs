using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Dominio.ObjetosDeValor
{
    public class Login
    {
        private Login()
        {
        }

        public Login(string valor)
        {
            var email = new Email.Email(valor);
            this.Valor = email.Endereco;
        }

        public string Valor { get; private set; }
    }
}
