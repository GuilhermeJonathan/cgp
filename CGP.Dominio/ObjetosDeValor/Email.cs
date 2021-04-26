using Cgp.Dominio.ObjetosDeValor.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor.Email
{
    public class Email
    {
        private Email()
        {
        }

        public Email(string valor)
        {
        //    if (!this.ValorValido(valor))
        //        throw new ExcecaoDeNegocio("Formato de email inválido");

            this.Endereco = valor;
        }

        public string Endereco { get; private set; }

        private bool ValorValido(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return false;

            var regExpEmail = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9._%+-]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            return regExpEmail.Match(valor).Success;
        }

        public override string ToString()
        {
            return this.Endereco;
        }

        public static Email Vazio => new Email(String.Empty);
    }
}
