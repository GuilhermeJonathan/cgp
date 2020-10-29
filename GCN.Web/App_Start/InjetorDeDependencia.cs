using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Container = SimpleInjector.Container;
using Campeonato.Aplicacao.GestaoDeFuncionarios;
using System.Web.Mvc;
using Campeonato.Aplicacao.Comum;
using Campeonato.Infraestrutura.ServicosExternos.Autenticacao.AutenticacaoViaCookieOwin;
using Campeonato.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using Campeonato.Aplicacao.Login;

namespace Campeonato.Web.App_Start
{
    public class InjetorDeDependencia
    {
        public static void Injetar()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<Contexto, Contexto>(Lifestyle.Scoped);

            container.Register<IServicoExternoDePersistenciaViaEntityFramework, ServicoExternoDePersistenciaViaEntityFramework>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeFuncionarios, ServicoDeGestaoDeFuncionarios>(Lifestyle.Scoped);
            container.Register<IServicoDeLogin, ServicoDeLogin>(Lifestyle.Scoped);
            container.Register<IServicoDeGeracaoDeHashSha, ServicoDeGeracaoDeHashSha>(Lifestyle.Scoped);
            container.Register<IServicoExternoDeAutenticacao, ServicoExternoDeAutenticacaoViaCookieOwin>(Lifestyle.Scoped);
            

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}