using Cgp.Aplicacao.GestaoDeComandosRegionais.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais
{
    public interface IServicoDeGestaoDeComandosRegionais
    {
        IList<ComandoRegional> RetonarTodosOsComandosRegionaisAtivos();
        string CadastrarComandoRegional(ModeloDeCadastroDeComandoRegional modelo, UsuarioLogado usuario);
        ModeloDeListaDeComandosRegionais RetonarComandosRegionaisPorFiltro(ModeloDeFiltroDeComandoRegional filtro, int pagina, int registrosPorPagina = 30);
        ModeloDeEdicaoDeComandoRegional BuscarComandoRegionalPorId(int id);
        string AlterarDadosDoComandoRegional(ModeloDeEdicaoDeComandoRegional modelo, UsuarioLogado usuario);
        string AtivarComando(int id, UsuarioLogado usuario);
    }
}
