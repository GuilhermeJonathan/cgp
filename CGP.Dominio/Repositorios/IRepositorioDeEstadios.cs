using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeEstadios : IRepositorio<Estadio>
    {
        IList<Estadio> RetornarTodosOsEstadios(string nome, int time, bool ativo, out int quantidadeEncontrada);
        IList<Estadio> RetornarTodosOsEstadiosAtivos();
        Estadio PegarPorId(int id);
    }
}
