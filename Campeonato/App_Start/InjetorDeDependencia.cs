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
using Campeonato.Aplicacao.GestaoDeTimes;
using Campeonato.Aplicacao.GestaoDeEstadio;
using Campeonato.Aplicacao.GestaoDeRodada;
using Campeonato.Aplicacao.GestaoDeJogos;
using Campeonato.Aplicacao.GestaoDeApostas.Modelos;
using Campeonato.Aplicacao.GestaoDeUsuarios;

namespace Campeonato.App_Start
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

            container.Register<IServicoDeGestaoDeTimes, ServicoDeGestaoDeTimes>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeEstadios, ServicoDeGestaoDeEstadios>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeRodadas, ServicoDeGestaoDeRodadas>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeJogos, ServicoDeGestaoDeJogos>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeApostas, ServicoDeGestaoDeApostas>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeUsuarios, ServicoDeGestaoDeUsuarios>(Lifestyle.Scoped);

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}