using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeUsuarios : Repositorio<Usuario>, IRepositorioDeUsuarios
    {
        public RepositorioDeUsuarios(Contexto contexto) : base(contexto) {}

        public Usuario PegarAtivoPorLogin(string login)
        {
            var usuario = this._contexto.Set<Usuario>().FirstOrDefault(
               au => au.Login.Valor == login);

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
        
        public IList<Usuario> RetornarUsuariosPorFiltro(string nome, string email, int batalhao, bool ativo, int pagina, int registrosPorPagina, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Usuario>()
                .Include(a => a.Batalhao)
                .AsQueryable();
             
            if(!String.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Valor.Contains(nome));

            if (!String.IsNullOrEmpty(email))
                query = query.Where(c => c.Login.Valor.Contains(email));

            if (batalhao > 0)
                query = query.Where(c => c.Batalhao.Id == batalhao);

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(i => i.Nome.Valor).Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
        }

        public int BuscarQtdUsuariosNovos()
        {
            var query = base._contexto.Set<Usuario>().AsQueryable();
            query = query.Where(i => i.UsuarioNovo);
            return query.Count();
        }

        public Usuario BuscarUsuarioCompletoPorId(int id)
        {
            var query = base._contexto.Set<Usuario>()
                .Include(a => a.Batalhao)
                .AsQueryable();
            return query.FirstOrDefault(a => a.Id == id);
        }
    }
}
