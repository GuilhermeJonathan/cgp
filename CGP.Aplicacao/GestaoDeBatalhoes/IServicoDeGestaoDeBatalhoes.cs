using Cgp.Aplicacao.GestaoDeBatalhoes.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeBatalhoes
{
    public interface IServicoDeGestaoDeBatalhoes
    {
        ModeloDeListaDeBatalhoes RetonarTodosOsBatalhoes(ModeloDeFiltroDeBatalhao filtro, int pagina, int registrosPorPagina = 30);
        IList<Batalhao> RetonarTodosOsBatalhoesAtivos();
        IList<ModeloDeBatalhoesDaLista> RetonarTodosOsTimesParaSelect();
        ModeloDeEdicaoDeBatalhao BuscarBatalhaoPorId(int id);
        string CadastrarBatalhao(ModeloDeCadastroDeBatalhao modelo, UsuarioLogado usuario);
        string AlterarDadosDoTime(ModeloDeEdicaoDeBatalhao modelo, UsuarioLogado usuario);
    }
}
