using Cgp.Aplicacao.GestaoDeTimes.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeTimes
{
    public interface IServicoDeGestaoDeTimes
    {
        ModeloDeListaDeTimes RetonarTodosOsTimes(ModeloDeFiltroDeTime filtro, int pagina, int registrosPorPagina = 30);
        IList<Time> RetonarTodosOsTimesAtivos();
        IList<ModeloDeTimesDaLista> RetonarTodosOsTimesParaSelect();
        ModeloDeEdicaoDeTime BuscarTimePorId(int id);
        string CadastrarTime(ModeloDeCadastroDeTime modelo, UsuarioLogado usuario);
        string AlterarDadosDoTime(ModeloDeEdicaoDeTime modelo, UsuarioLogado usuario);
    }
}
