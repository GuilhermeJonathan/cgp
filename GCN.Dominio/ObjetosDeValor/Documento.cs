using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Dominio.ObjetosDeValor
{
    public class Documento
    {
        private Documento()
        {

        }

        public Documento(string numero)
        {
            if (string.IsNullOrEmpty(numero) || numero.Length < 11)
                throw new ExcecaoDeNegocio("Documento inválido. Verifique se o documento é um CPF ou CNPJ válido");

            numero = numero.Replace(".", "").Replace("/", "").Replace("-", "");
            if (numero.Length == 11)
                this.Numero = new Cpf(numero).Codigo;
            else if (numero.Length == 14)
                this.Numero = new Cnpj(numero).Codigo;
        }

        public bool EhUmCpf => this.Numero.Length == 11;
        public bool EhUmCnpj => this.Numero.Length == 14;

        public string Numero { get; private set; }
    }
}
