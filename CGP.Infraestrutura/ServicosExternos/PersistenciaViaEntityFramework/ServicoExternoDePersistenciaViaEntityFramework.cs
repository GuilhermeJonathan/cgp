using Cgp.Dominio.Repositorios;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework
{
    public class ServicoExternoDePersistenciaViaEntityFramework : IServicoExternoDePersistenciaViaEntityFramework
    {
        private readonly Contexto _contexto;

        public ServicoExternoDePersistenciaViaEntityFramework(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public IRepositorioDeFuncionarios RepositorioDeFuncionarios
        {
            get
            {
                return new RepositorioDeFuncionarios(this._contexto);
            }
        }

        public IRepositorioDeUsuarios RepositorioDeUsuarios
        {
            get
            {
                return new RepositorioDeUsuarios(this._contexto);
            }
        }

        public IRepositorioDeBatalhoes RepositorioDeBatalhoes
        {
            get
            {
                return new RepositorioDeBatalhoes(this._contexto);
            }
        }

        public IRepositorioDeComandosRegionais RepositorioDeComandosRegionais
        {
            get
            {
                return new RepositorioDeComandosRegionais(this._contexto);
            }
        }

        public IRepositorioDeCidades RepositorioDeCidades
        {
            get
            {
                return new RepositorioDeCidades(this._contexto);
            }
        }

        public IRepositorioDeUfs RepositorioDeUfs
        {
            get
            {
                return new RepositorioDeUfs(this._contexto);
            }
        }

        public IRepositorioDeVeiculos RepositorioDeCarros
        {
            get
            {
                return new RepositorioDeVeiculos(this._contexto);
            }
        }

        public IRepositorioDeCaraters RepositorioDeCaraters
        {
            get
            {
                return new RepositorioDeCaraters(this._contexto);
            }
        }

        public IRepositorioDeCrimes RepositorioDeCrimes
        {
            get
            {
                return new RepositorioDeCrimes(this._contexto);
            }
        }

        public void Persistir()
        {
            this._contexto.SaveChanges();
        }

        public void Dispose()
        {
            if (this._contexto != null)
                this._contexto.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
