using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeCameras : IRepositorio<Camera>
    {
        IList<Camera> RetornarCamerasPorFiltro(string nome, int cidade, bool ativo, out int quantidadeEncontrada);
        Camera PegarPorEndereco(string endereco);
        Camera PegarPorId(int id);
    }
}
