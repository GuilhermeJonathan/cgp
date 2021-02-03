using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios
{
    public interface IServicoDeGestaoDeUsuarios
    {
        string CadastrarNovoUsuario(ModeloDeCadastroDeUsuario modelo);
        string AlterarDadosDoUsuario(ModeloDeEdicaoDeUsuario modelo, UsuarioLogado usuario);
        IList<ModeloDeUsuarioDaLista> RetonarTodosOsUsuariosAtivos();
        ModeloDeListaDeUsuarios RetonarUsuariosPorFiltro(ModeloDeFiltroDeUsuario filtro, int pagina, int registrosPorPagina = 30);
        string AtivarUsuario(int id, UsuarioLogado usuario);
        string CadastrarSaldo(int id, decimal saldo, UsuarioLogado usuario, string tipoDeSolicitacaoFinanceira);
        int BuscarUsuariosNovos();
        ModeloDeEdicaoDeUsuario BuscarUsuarioPorId(int id);
        ModeloDeEdicaoDeUsuario BuscarUsuarioComHistoricoPorId(int id);
    }
}
