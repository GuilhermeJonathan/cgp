using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeApostas.Modelos
{
    public interface IServicoDeGestaoDeApostas
    {
        ModeloDeListaDeApostas BuscarApostasPorFiltro(ModeloDeFiltroDeAposta filtro, int pagina, int registrosPorPagina = 30);
        ModeloDeEdicaoDeAposta BuscarApostaPorRodada(int idRodada, UsuarioLogado usuario);
        string SalvarMinhaAposta(int id, int[] placar1, int[] placar2, int[] idJogos, UsuarioLogado usuario);
        ModeloDeEdicaoDeAposta VisualizarAposta(int idRodada, int idUsuario);
        ModeloDeListaDeApostas BuscarResultado();
    }
}
