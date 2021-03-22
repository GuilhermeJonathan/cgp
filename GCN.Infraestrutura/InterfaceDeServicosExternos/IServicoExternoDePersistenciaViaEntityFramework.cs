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
        IRepositorioDeTimes RepositorioDeTimes { get; }
        IRepositorioDeEstadios RepositorioDeEstadios { get; }
        IRepositorioDeRodadas RepositorioDeRodadas { get; }
        IRepositorioDeJogos RepositorioDeJogos { get; }
        IRepositorioDeApostas RepositorioDeApostas { get; }
        IRepositorioDePremiacoes RepositorioDePremiacoes { get; }
        IRepositorioDeTemporadas RepositorioDeTemporadas { get; }
        void Persistir();
        void Dispose();
    }
}
