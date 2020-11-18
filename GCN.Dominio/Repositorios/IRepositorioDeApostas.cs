using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Repositorios
{
    public interface IRepositorioDeApostas : IRepositorio<Aposta>
    {
        IList<Aposta> RetornarApostasPorFiltro(int usuario, int rodada, out int quantidadeEncontrada);
        IList<Aposta> RetornarApostasPorRodada(int rodada);
        Aposta PegarPorId(int id);
        Aposta PegarPorIdRodadaEUsuario(int id, int usuario);
        IList<Aposta> RetornarApostasParaResultado(int idRodada);
        IList<JogoDaAposta> RetornarJogosDaApostaParaAlterar(int idRodada, int time1, int time2);
        Aposta PegarRodadaExclusiva(int idUsuario, int idRodada = 0);
    }
}
