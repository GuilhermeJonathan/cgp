using Cgp.Aplicacao.GestaoDePremiacoes.Modelos;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDePremiacoes
{
    public interface IServicoDeGestaoDePremiacoes
    {
        ModeloDeListaDePremiacoes RetonarPremiacoesPorFiltro(ModeloDeFiltroDePremiacao filtro, int pagina, int registrosPorPagina = 30);
        string CadastrarPremiacao(ModeloDeCadastroDePremiacao modelo, UsuarioLogado usuario);
        ModeloDeEdicaoDePremiacao BuscarPremiacaoPorId(int id);
    }
}
