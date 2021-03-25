using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCarros : Repositorio<Carro>, IRepositorioDeCarros
    {
        public RepositorioDeCarros(Contexto contexto) : base(contexto) { }

        public Carro PegarPorId(int id)
        {
            return this._contexto.Set<Carro>()
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
