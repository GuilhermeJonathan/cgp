using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUfs
{
    public interface IServicoDeGestaoDeUfs
    {
        IList<Uf> RetonarTodasUfs();
    }
}
