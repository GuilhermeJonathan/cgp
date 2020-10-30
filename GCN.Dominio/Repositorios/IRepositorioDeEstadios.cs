using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Repositorios
{
    public interface IRepositorioDeEstadios
    {
        IList<Estadio> RetornarTodosOsEstadios(string nome, int time, bool ativo, out int quantidadeEncontrada);
        Estadio PegarPorId(int id);
    }
}
