using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeJogos : IRepositorio<Jogo>
    {
        IList<Jogo> RetornarJogosPorFiltro(int time, int temporada, int rodada, DateTime dataDoJogo, out int quantidadeEncontrada);
        IList<Jogo> RetornarTodosJogos();
        IList<Jogo> RetornarJogosPorRodada(int rodada);
        Jogo PegarPorId(int id);
        JogoDaAposta PegarJogoDaApostaPorId(int id);
    }
}
