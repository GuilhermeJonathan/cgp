using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public enum SituacaoDoCarater
    {
        [Description("Cadastrado")]
        Cadastrado = 1,
        Localizado = 2,
        [Description("Baixa Automática")]
        BaixaAutomatica = 3,
        [Description("Excluído")]
        Excluido = 4
    }
}
