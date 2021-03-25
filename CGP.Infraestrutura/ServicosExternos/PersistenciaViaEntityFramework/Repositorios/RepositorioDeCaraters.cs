using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeCaraters : Repositorio<Carater>, IRepositorioDeCaraters
    {
        public RepositorioDeCaraters(Contexto contexto) : base(contexto) { }

        public Carater PegarPorId(int id)
        {
            return this._contexto.Set<Carater>()
                .Include(a => a.Veiculo)
                .Include(a => a.Cidade)
                .Include(a => a.Crime)
                .FirstOrDefault(a => a.Id == id);
        }
    }
}
