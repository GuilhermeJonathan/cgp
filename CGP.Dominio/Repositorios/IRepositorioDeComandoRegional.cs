using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeComandosRegionais : IRepositorio<ComandoRegional>
    {
        IList<ComandoRegional> RetornarTodosOsComandosRegionaisPorFiltro(string nome, bool ativo, out int quantidadeEncontrada);
        IList<ComandoRegional> RetornarTodosOsComandosRegionaisAtivos();
        ComandoRegional PegarPorId(int id);
    }
}
