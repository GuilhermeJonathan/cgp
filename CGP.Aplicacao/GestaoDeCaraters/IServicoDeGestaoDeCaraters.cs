using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters
{
    public interface IServicoDeGestaoDeCaraters
    {
        ModeloDeListaDeCaraters RetonarCaratersPorFiltro(ModeloDeFiltroDeCarater filtro, int pagina, int registrosPorPagina = 30);
        ModeloDeListaDeCaraters RetonarCaratersPorCidades(ModeloDeFiltroDeCarater filtro);
        ModeloDeEdicaoDeCarater BuscarCaraterPorId(int id);
        string CadastrarCarater(ModeloDeCadastroDeCarater modelo, UsuarioLogado usuario);
        string AlterarDadosDoCarater(ModeloDeEdicaoDeCarater modelo, UsuarioLogado usuario);
        string RealizarBaixaVeiculo(int id, string descricao, int cidade, UsuarioLogado usuario);
    }
}
