using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class Nome
    {
        public Nome()
        {

        }

        public Nome(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ExcecaoDeNegocio("Não é possível criar um nome vazio");

            Valor = valor;
        }

        public string Valor { get; private set; }
    }
}
