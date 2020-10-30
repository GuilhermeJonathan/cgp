using Campeonato.Aplicacao.GestaoDeRodada.Modelos;
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
        ModeloDeEdicaoDeRodada BuscarRodadaPorId(int id);
        string CadastrarRodada(ModeloDeCadastroDeRodada modelo, UsuarioLogado usuario);
        string AlterarDadosDaRodada(ModeloDeEdicaoDeRodada modelo, UsuarioLogado usuario);
    }
}
