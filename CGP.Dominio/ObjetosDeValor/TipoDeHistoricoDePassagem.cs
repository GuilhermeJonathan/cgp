using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public enum TipoDeHistoricoDePassagem
    {
        [Description("Automático")]
        Automatico = 1,
        [Description("Manual")]
        Manual = 2
    }
}
