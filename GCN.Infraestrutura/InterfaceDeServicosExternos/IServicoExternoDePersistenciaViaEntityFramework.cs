using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.InterfaceDeServicosExternos
{
    public interface IServicoExternoDePersistenciaViaEntityFramework
    {
        IRepositorioDeFuncionarios RepositorioDeFuncionarios { get; }
        IRepositorioDeUsuarios RepositorioDeUsuarios { get; }
        IRepositorioDeTimes RepositorioDeTimes { get; }
        IRepositorioDeEstadios RepositorioDeEstadios { get; }

        void Persistir();
        void Dispose();
    }
}
