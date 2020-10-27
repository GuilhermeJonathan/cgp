using GCN.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Infraestrutura.InterfaceDeServicosExternos
{
    public interface IServicoExternoDePersistenciaViaEntityFramework
    {
        IRepositorioDeFuncionarios RepositorioDeFuncionarios { get; }
        IRepositorioDeUsuarios RepositorioDeUsuarios { get; }

        void Persistir();
        void Dispose();
    }
}
