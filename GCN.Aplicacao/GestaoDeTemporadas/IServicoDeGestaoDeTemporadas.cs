using Cgp.Aplicacao.GestaoDeTemporadas.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeTemporadas
{
    public interface IServicoDeGestaoDeTemporadas
    {
        ModeloDeListaDeTemporada RetornarTemporadasPorFiltro(ModeloDeFiltroDeTemporada filtro, int pagina, int registrosPorPagina = 30);
        string CadastrarTemporada(ModeloDeCadastroDeTemporada modelo, UsuarioLogado usuario);
        ModeloDeEdicaoDeTemporada BuscarTemporadaPorId(int id);
        string AlterarDadosDaTemporada(ModeloDeEdicaoDeTemporada modelo, UsuarioLogado usuario);
        IList<Temporada> RetonarTodasAsTemporadasAtivas();
        string AtivarTemporada(int id, UsuarioLogado usuario);
        int BuscarTemporadaAtiva();
    }
}
