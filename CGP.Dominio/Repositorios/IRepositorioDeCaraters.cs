using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeCaraters : IRepositorio<Carater>
    {
        IList<Carater> RetornarCaratersPorFiltro(int cidade, int crime, out int quantidadeEncontrada);
        Carater PegarPorId(int id);
    }
}
