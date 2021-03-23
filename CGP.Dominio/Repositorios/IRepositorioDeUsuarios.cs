using Cgp.Dominio.Entidades;
using System.Collections.Generic;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeUsuarios : IRepositorio<Usuario>
    {
        Usuario PegarAtivoPorLogin(string login);
        Usuario PegarPorLoginESenha(string login, string senha);
        IList<Usuario> RetornarTodosUsuarios();
        IList<HistoricoFinanceiro> RetornarHistoricosFinanceirosDeSaques();
        IList<Usuario> RetornarUsuariosPorFiltro(string nome, string email, int batalhao, bool ativo, int pagina, int registrosPorPagina, out int quantidadeEncontrada);
        int BuscarQtdUsuariosNovos();
        Usuario BuscarUsuarioComHistorico(int id);
        HistoricoFinanceiro BuscarHistoricoComUsuario(int id);
    }
}
