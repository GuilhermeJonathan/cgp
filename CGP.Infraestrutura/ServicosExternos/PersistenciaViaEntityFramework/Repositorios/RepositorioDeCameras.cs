using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCameras : Repositorio<Camera>, IRepositorioDeCameras
    {
        public RepositorioDeCameras(Contexto contexto) : base(contexto) { }

        public Camera PegarPorEndereco(string endereco)
        {
            return this._contexto.Set<Camera>()
                .Include(a => a.Cidade)
                .FirstOrDefault(a => a.Endereco == endereco);
        }
    }
}
