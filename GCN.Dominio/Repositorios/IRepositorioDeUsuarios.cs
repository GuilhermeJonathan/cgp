using GCN.Dominio.Entidades;

namespace GCN.Dominio.Repositorios
{
    public interface IRepositorioDeUsuarios : IRepositorio<Usuario>
    {
        Usuario PegarAtivoPorLogin(string login);
        Usuario PegarPorLoginESenha(string login, string senha);
    }
}
