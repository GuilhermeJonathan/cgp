using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public enum TipoDeHistoricoDeCarater
    {
        [Description("Criação")]
        Criacao = 1,
        [Description("Historico")]
        Historico = 2,
        [Description("Passagem")]
        Passagem = 3,
        [Description("Foto")]
        Foto = 4,
        [Description("Baixa")]
        Baixa = 5,
    }
}
