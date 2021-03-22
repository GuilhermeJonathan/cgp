using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDePremiacoes : IRepositorio<Premiacao>
    {
        IList<Premiacao> RetornarPremiacoesPorFiltro(int rodada, int usuario, int pagina, int registrosPorPagina, out int quantidadeEncontrada);
        IList<Premiacao> RetornarPremiacoesPorTemporada(int usuario);
        Premiacao PegarPorId(int id);
    }
}
