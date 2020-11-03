using Campeonato.Dominio.Entidades;
using System.Collections.Generic;

namespace Campeonato.Dominio.Repositorios
{
    public interface IRepositorioDeUsuarios : IRepositorio<Usuario>
    {
        Usuario PegarAtivoPorLogin(string login);
        Usuario PegarPorLoginESenha(string login, string senha);
        IList<Usuario> RetornarTodosUsuarios();
    }
}
