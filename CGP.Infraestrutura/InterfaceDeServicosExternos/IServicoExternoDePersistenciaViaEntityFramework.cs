using Cgp.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.InterfaceDeServicosExternos
{
    public interface IServicoExternoDePersistenciaViaEntityFramework
    {
        IRepositorioDeFuncionarios RepositorioDeFuncionarios { get; }
        IRepositorioDeUsuarios RepositorioDeUsuarios { get; }
        IRepositorioDeBatalhoes RepositorioDeBatalhoes { get; }
        IRepositorioDeComandosRegionais RepositorioDeComandosRegionais { get; }
        IRepositorioDeCidades RepositorioDeCidades { get; }
        IRepositorioDeUfs RepositorioDeUfs { get; }
        IRepositorioDeVeiculos RepositorioDeCarros { get; }
        IRepositorioDeCrimes RepositorioDeCrimes { get; }
        IRepositorioDeCaraters RepositorioDeCaraters { get; }
        IRepositorioDeVeiculos RepositorioDeVeiculos { get; }
        void Persistir();
        void Dispose();
    }
}
