using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class Telefone
    {
        private Telefone()
        {
        }

        public Telefone(string numero)
        {
            this.Numero = numero;
        }

        public Telefone(string ddd, string numero)
        {
            if (!TelefoneValido(ddd, numero))
                throw new ExcecaoDeNegocio("Telefone inválido");

            this.Numero = numero;
            this.Ddd = ddd;
        }

        private static bool TelefoneValido(string ddd, string numero)
        {
            return !string.IsNullOrEmpty(ddd) && ddd.Length >= 2 && !string.IsNullOrEmpty(numero) && numero.Length > 7;
        }

        public string Numero { get; set; }
        public string Ddd { get; set; }

        public static Telefone Vazio => new Telefone("");

    }
}
