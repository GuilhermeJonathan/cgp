using Cgp.Aplicacao.GestaoDeRodada.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeRodada
{
    public interface IServicoDeGestaoDeRodadas
    {
        ModeloDeListaDeRodadas RetonarTodosasRodadas(ModeloDeFiltroDeRodada filtro, int pagina, int registrosPorPagina = 30);
        IList<Rodada> RetonarTodosAsRodadasAtivas();
        IList<Rodada> RetonarTodosAsRodadasAtivasPorTemporada(int temporada);
        ModeloDeEdicaoDeRodada BuscarRodadaPorId(int id);
        string CadastrarRodada(ModeloDeCadastroDeRodada modelo, UsuarioLogado usuario);
        string AlterarDadosDaRodada(ModeloDeEdicaoDeRodada modelo, UsuarioLogado usuario);
        int BuscarRodadaAtiva();
        string CadastrarResultados(int id, string[] placar1, string[] placar2, int[] idJogos, UsuarioLogado usuario);
        string FecharRodada(int id, UsuarioLogado usuario);
        ModeloDeEdicaoDeRodada BuscarRodadaParaPremiacao(int id);
    }
}
