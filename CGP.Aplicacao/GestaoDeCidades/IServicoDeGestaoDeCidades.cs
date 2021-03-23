using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCidades
{
    public interface IServicoDeGestaoDeCidades
    {
        IList<Cidade> RetonarCidadesPorUf(int uf);
    }
}
