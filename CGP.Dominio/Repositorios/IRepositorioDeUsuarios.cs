using Cgp.Dominio.Entidades;
using System.Collections.Generic;

namespace Cgp.Dominio.Repositorios
{
    public interface IRepositorioDeUsuarios : IRepositorio<Usuario>
    {
        Usuario PegarAtivoPorLogin(string login);
        Usuario PegarPorLoginESenha(string login, string senha);
        Usuario PegarPorMatricula(string matricula);
        IList<Usuario> RetornarTodosUsuarios();
        IList<Usuario> RetornarUsuariosPorFiltro(string nome, string email, int batalhao, bool ativo, bool ehAtenas, int pagina, int registrosPorPagina, out int quantidadeEncontrada);
        int BuscarQtdUsuariosNovos();
        Usuario BuscarUsuarioCompletoPorId(int id);
    }
}
