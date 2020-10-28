using GCN.Infraestrutura.InterfaceDeServicosExternos;
using GCN.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Container = SimpleInjector.Container;
using GCN.Aplicacao.GestaoDeFuncionarios;
using System.Web.Mvc;
using GCN.Aplicacao.Comum;
using GCN.Infraestrutura.ServicosExternos.Autenticacao.AutenticacaoViaCookieOwin;
using GCN.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using GCN.Aplicacao.Login;

namespace GCN.Web.App_Start
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