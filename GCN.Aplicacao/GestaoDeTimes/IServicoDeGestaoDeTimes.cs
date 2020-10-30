using Campeonato.Aplicacao.GestaoDeTimes.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTimes
{
    public interface IServicoDeGestaoDeTimes
    {
        ModeloDeListaDeTimes RetonarTodosOsTimes(ModeloDeFiltroDeTime filtro, int pagina, int registrosPorPagina = 30);
        IList<Time> RetonarTodosOsTimesAtivos();
        ModeloDeEdicaoDeTime BuscarTimePorId(int id);
        string AlterarDadosDoTime(ModeloDeEdicaoDeTime modelo, UsuarioLogado usuario);
    }
}
