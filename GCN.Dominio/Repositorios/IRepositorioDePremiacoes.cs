using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Repositorios
{
    public interface IRepositorioDePremiacoes : IRepositorio<Premiacao>
    {
        IList<Premiacao> RetornarPremiacoesPorFiltro(int rodada, int usuario, int pagina, int registrosPorPagina, out int quantidadeEncontrada);
        Premiacao PegarPorId(int id);
    }
}
