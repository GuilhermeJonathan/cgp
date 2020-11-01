using Campeonato.Aplicacao.GestaoDeRodada.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeRodada
{
    public interface IServicoDeGestaoDeRodadas
    {
        ModeloDeListaDeRodadas RetonarTodosasRodadas(ModeloDeFiltroDeRodada filtro, int pagina, int registrosPorPagina = 30);
        IList<Rodada> RetonarTodosAsRodadasAtivas();
        ModeloDeEdicaoDeRodada BuscarRodadaPorId(int id);
        string CadastrarRodada(ModeloDeCadastroDeRodada modelo, UsuarioLogado usuario);
        string AlterarDadosDaRodada(ModeloDeEdicaoDeRodada modelo, UsuarioLogado usuario);
        int BuscarRodadaAtiva();
        string CadastrarResultados(int id, int[] placar1, int[] placar2, int[] idJogos, UsuarioLogado usuario);
    }
}
