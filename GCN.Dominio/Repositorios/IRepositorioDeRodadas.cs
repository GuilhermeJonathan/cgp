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
        IList<Rodada> RetornarTodosAsRodadas(int nome, int temporada, out int quantidadeEncontrada);
        IList<Rodada> RetornarTodosAsRodadasAtivas();
        IList<Rodada> RetornarTodosAsRodadasAtivasPorTemporada(int temporada);
        Rodada PegarPorId(int id);
        int BuscarRodadaAtiva();
        Rodada BuscarProximaRodada();
    }
}
