using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeUsuarios : Repositorio<Usuario>, IRepositorioDeUsuarios
    {
        public RepositorioDeUsuarios(Contexto contexto) : base(contexto) {}

        public Usuario PegarAtivoPorLogin(string login)
        {
            var usuario = this._contexto.Set<Usuario>().FirstOrDefault(
               au => au.Login.Valor == login && au.Ativo == true);

            return usuario != null ? usuario : null;
        }

        public Usuario PegarPorLoginESenha(string login, string senha)
        {
            var usuario = this._contexto.Set<Usuario>().FirstOrDefault(
            au => au.Login.Valor == login && au.Senha.Valor == senha && au.Ativo == true);

            return usuario != null ? usuario : null;
        }

        public IList<Usuario> RetornarTodosUsuarios()
        {
            var query = this._contexto.Set<Usuario>()
                .AsQueryable();

            query = query.Where(c => c.Ativo);

            return query.OrderBy(a => a.DataDoCadastro).ToList();
        }
    }
}
