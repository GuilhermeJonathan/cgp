using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Repositorios
{
    public interface IRepositorioDeTimes : IRepositorio<Time>
    {
        IList<Time> RetornarTodosOsTimes(string nome, bool ativo, out int quantidadeEncontrada);
        IList<Time> RetornarTodosOsTimesAtivos();
        Time PegarPorId(int id);
    }
}
