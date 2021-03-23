using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais
{
    public interface IServicoDeGestaoDeComandosRegionais
    {
        IList<ComandoRegional> RetonarTodosOsComandosRegionaisAtivos();
    }
}
