using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Repositorios
{
    public interface IRepositorioDeRodadas : IRepositorio<Rodada>
    {
        IList<Rodada> RetornarTodosAsRodadas(string nome, string temporada, bool ativo, out int quantidadeEncontrada);
        Rodada PegarPorId(int id);
    }
}
