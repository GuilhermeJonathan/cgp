using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeVeiculos : Repositorio<Veiculo>, IRepositorioDeVeiculos
    {
        public RepositorioDeVeiculos(Contexto contexto) : base(contexto) { }

        public Veiculo PegarPorId(int id)
        {
            return this._contexto.Set<Veiculo>()
                .FirstOrDefault(a => a.Id == id);
        }

        public Veiculo PegarPorPlaca(string placa)
        {
            return this._contexto.Set<Veiculo>()
                .Include(a => a.Proprietario)
                .Include(a => a.Possuidor)
                .FirstOrDefault(a => a.Placa.Contains(placa));
        }
    }
}
