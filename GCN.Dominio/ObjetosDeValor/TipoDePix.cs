using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public enum TipoDePix
    {
        [Description("CPF ou CNPJ")]
        CpfCnpj = 1,
        Celular = 2,
        Email = 3,
        [Description("Chave Aleatória")]
        ChaveAleatoria = 4
    }
}
