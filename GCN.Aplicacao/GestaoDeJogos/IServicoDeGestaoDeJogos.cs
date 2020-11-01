using Campeonato.Aplicacao.GestaoDeJogos.Modelos;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeJogos
{
    public interface IServicoDeGestaoDeJogos
    {
        ModeloDeListaDeJogos RetonarTodosOsJogos(ModeloDeFiltroDeJogo filtro, int pagina, int registrosPorPagina = 30);
        string CadastrarJogo(ModeloDeCadastroDeJogo modelo, UsuarioLogado usuario);
        ModeloDeEdicaoDeJogo BuscarJogoPorId(int id);
        string AlterarDadosDoJogo(ModeloDeEdicaoDeJogo modelo, UsuarioLogado usuario);
    }
}
