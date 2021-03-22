using Cgp.Aplicacao.GestaoDeEstadio.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeEstadio
{
    public interface IServicoDeGestaoDeEstadios
    {
        ModeloDeListaDeEstadios RetonarTodosOsEstadios(ModeloDeFiltroDeEstadio filtro, int pagina, int registrosPorPagina = 30);
        IList<Estadio> RetonarTodosOsEstadiosAtivos();
        ModeloDeEdicaoDeEstadio BuscarEstadioPorId(int id);
        string CadastrarEstadio(ModeloDeCadastroDeEstadio modelo, UsuarioLogado usuario);
        string AlterarDadosDoEstadio(ModeloDeEdicaoDeEstadio modelo, UsuarioLogado usuario);
    }
}
