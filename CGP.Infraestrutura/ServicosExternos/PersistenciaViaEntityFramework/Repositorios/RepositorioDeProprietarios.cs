using Cgp.Dominio.Entidades;
using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeProprietarios : Repositorio<Proprietario>, IRepositorioDeProprietarios
    {
        public RepositorioDeProprietarios(Contexto contexto) : base(contexto) { }

        public Proprietario PegarPorDocumento(string documento)
        {
            return this._contexto.Set<Proprietario>()
                .FirstOrDefault(a => a.Documento == documento);
        }
    }
}
