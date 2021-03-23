using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeTemporadas : IRepositorio<Temporada>
    {
        IList<Temporada> RetornarTemporadasPorFiltro(string nome, string ano, string pais, bool ativo, out int quantidadeEncontrada);
        Temporada PegarPorId(int id);
        IList<Temporada> RetornarTodosAsTemporadasAtivas();
        Temporada BuscarTemporadaAtiva();
    }
}
