using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public enum SituacaoDoAlerta
    {
        [Description("Cadastrado")]
        Cadastrado = 1,
        [Description("Finalizado")]
        Finalizado = 2        
    }
}
