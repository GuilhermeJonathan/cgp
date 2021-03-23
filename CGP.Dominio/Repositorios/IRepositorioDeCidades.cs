using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeCidades
    {
        IList<Cidade> RetornarCidadesPorUf(int uf);
        Cidade PegarPorId(int id);
    }
}
