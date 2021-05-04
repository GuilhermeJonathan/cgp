using Cgp.Aplicacao.Processos.InterfaceDeServicos;
using Cgp.Aplicacao.Processos.Servicos;
using Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using SimpleInjector;

namespace Cgp.Processos
{
    public class Startup
    {
        static Container container;
        public static void Injetar()
        {
            container = new Container();
            container.Register<Contexto, Contexto>(Lifestyle.Scoped);
            container.Register<IServicoDeBuscaDeCaraters, ServicoDeBuscaDeCaraters>(Lifestyle.Scoped);
            container.Verify();
        }
    }
}
