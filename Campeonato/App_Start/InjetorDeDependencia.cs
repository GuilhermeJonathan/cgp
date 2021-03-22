using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Container = SimpleInjector.Container;
using Cgp.Aplicacao.GestaoDeFuncionarios;
using System.Web.Mvc;
using Cgp.Aplicacao.Comum;
using Cgp.Infraestrutura.ServicosExternos.Autenticacao.AutenticacaoViaCookieOwin;
using Cgp.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using Cgp.Aplicacao.Login;
using Cgp.Aplicacao.GestaoDeTimes;
using Cgp.Aplicacao.GestaoDeEstadio;
using Cgp.Aplicacao.GestaoDeRodada;
using Cgp.Aplicacao.GestaoDeJogos;
using Cgp.Aplicacao.GestaoDeApostas.Modelos;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDePremiacoes;
using Cgp.Infraestrutura.ServicosExternos.GeracaoDocumentoEmPDF;
using Cgp.Infraestrutura.ServicosExternos.ArmazenamentoEmNuvem;
using Cgp.Aplicacao.Util;
using Cgp.Aplicacao.GestaoDeDashboard;
using Cgp.SendGrid;
using Cgp.Aplicacao.MontagemDeEmails;
using Cgp.Aplicacao.GestaoDeTemporadas;

namespace Cgp.App_Start
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
            container.Register<IServicoDeGestaoDePremiacoes, ServicoDeGestaoDePremiacoes>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeDashboard, ServicoDeGestaoDeDashboard>(Lifestyle.Scoped);
            container.Register<IServicoDeGeracaoDeDocumentosEmPdf, ServicoDeGeracaoDeDocumentosEmPdf>(Lifestyle.Scoped);
            container.Register<IServicoDeEnvioDeEmails, ServicoDeEnvioDeEmails>(Lifestyle.Scoped);
            container.Register<IServicoDeMontagemDeEmails, ServicoDeMontagemDeEmails>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeTemporadas, ServicoDeGestaoDeTemporadas>(Lifestyle.Scoped);

            container.Register<IServicoExternoDeArmazenamentoEmNuvem>(() => new ServicoExternoDeArmazenamentoEmNuvem(
               VariaveisDeAmbiente.Pegar<string>("azure:contaDeArmazenamentoAzure"), VariaveisDeAmbiente.Pegar<string>("azure:chaveDaContaDeArmazenamentoAzure")));

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}