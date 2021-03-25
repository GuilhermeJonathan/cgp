using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCrimes : Repositorio<Crime>, IRepositorioDeCrimes
    {
        public RepositorioDeCrimes(Contexto contexto) : base(contexto) { }
        public Crime PegarPorId(int id)
        {
            return this._contexto.Set<Crime>()
                .FirstOrDefault(a => a.Id == id);
        }

    }
}
