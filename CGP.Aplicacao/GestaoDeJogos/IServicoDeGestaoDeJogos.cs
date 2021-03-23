using Cgp.Aplicacao.GestaoDeJogos.Modelos;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeJogos
{
    public interface IServicoDeGestaoDeJogos
    {
        ModeloDeListaDeJogos RetonarTodosOsJogos(ModeloDeFiltroDeJogo filtro, int pagina, int registrosPorPagina = 30);
        string CadastrarJogo(ModeloDeCadastroDeJogo modelo, UsuarioLogado usuario);
        ModeloDeEdicaoDeJogo BuscarJogoPorId(int id);
        string AlterarDadosDoJogo(ModeloDeEdicaoDeJogo modelo, UsuarioLogado usuario);
    }
}
