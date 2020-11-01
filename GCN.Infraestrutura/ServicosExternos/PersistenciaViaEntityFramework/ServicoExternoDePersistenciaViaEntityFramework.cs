using Campeonato.Dominio.Repositorios;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework
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

        public IRepositorioDeTimes RepositorioDeTimes
        {
            get
            {
                return new RepositorioDeTime(this._contexto);
            }
        }

        public IRepositorioDeEstadios RepositorioDeEstadios
        {
            get
            {
                return new RepositorioDeEstadio(this._contexto);
            }
        }

        public IRepositorioDeRodadas RepositorioDeRodadas
        {
            get
            {
                return new RepositorioDeRodada(this._contexto);
            }
        }

        public IRepositorioDeJogos RepositorioDeJogos
        {
            get
            {
                return new RepositorioDeJogo(this._contexto);
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
