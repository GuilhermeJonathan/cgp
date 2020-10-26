using GCN.Dominio;
using GCN.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeClientes : Repositorio<Cliente>, IRepositorioDeClientes
    {
        private readonly Contexto _contexto;

        public RepositorioDeClientes(Contexto contexto) : base(contexto)
        {
            this._contexto = contexto;
        }

        public bool VerificaSeJaCliente(string nome)
        {
            var usuario = this._contexto.Set<Cliente>().FirstOrDefault(u => u.Nome == nome);
            return usuario == null ? false : true;
        }
    }
}
