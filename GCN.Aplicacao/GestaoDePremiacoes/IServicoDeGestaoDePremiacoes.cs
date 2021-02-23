using Campeonato.Aplicacao.GestaoDePremiacoes.Modelos;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDePremiacoes
{
    public interface IServicoDeGestaoDePremiacoes
    {
        ModeloDeListaDePremiacoes RetonarPremiacoesPorFiltro(ModeloDeFiltroDePremiacao filtro, int pagina, int registrosPorPagina = 30);
        string CadastrarPremiacao(ModeloDeCadastroDePremiacao modelo, UsuarioLogado usuario);
        ModeloDeEdicaoDePremiacao BuscarPremiacaoPorId(int id);
    }
}
