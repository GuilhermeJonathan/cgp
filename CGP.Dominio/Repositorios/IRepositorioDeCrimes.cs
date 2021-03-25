using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeCrimes : IRepositorio<Crime>
    {
        Crime PegarPorId(int id);
        IList<Crime> RetornarCrimesPorFiltro(string nome, string artigo, bool ativo, out int quantidadeEncontrada);
        IList<Crime> RetornarTodosOsCrimesAtivos();
    }
}
