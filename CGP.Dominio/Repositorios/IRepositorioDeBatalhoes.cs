using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeBatalhoes : IRepositorio<Batalhao>
    {
        IList<Batalhao> RetornarTodosOsBatalhoes(string nome, int comandoRegional, bool ativo, out int quantidadeEncontrada);
        IList<Batalhao> RetornarTodosOsBatalhoesAtivos();
        Batalhao PegarPorId(int id);
    }
}
