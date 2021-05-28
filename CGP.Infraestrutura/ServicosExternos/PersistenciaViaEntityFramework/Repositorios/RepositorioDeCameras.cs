using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCameras : Repositorio<Camera>, IRepositorioDeCameras
    {
        public RepositorioDeCameras(Contexto contexto) : base(contexto) { }

        public IList<Camera> RetornarCamerasPorFiltro(string nome, int cidade, bool ativo, out int quantidadeEncontrada)
        {
            var query = this._contexto.Set<Camera>()
                .Include(a => a.Cidade)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (cidade > 0)
                query = query.Where(c => c.Cidade.Id == cidade);

            query = query.Where(c => c.Ativo == ativo);

            quantidadeEncontrada = query.Count();

            return query.OrderBy(a => a.Nome).ToList();
        }

        public Camera PegarPorEndereco(string endereco)
        {
            return this._contexto.Set<Camera>()
                .Include(a => a.Cidade)
                .FirstOrDefault(a => a.Nome == endereco);
        }

        public Camera PegarPorId(int id)
        {
            return this._contexto.Set<Camera>()
                .Include(a => a.Cidade)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
