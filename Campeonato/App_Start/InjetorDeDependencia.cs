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
using Campeonato.Aplicacao.GestaoDePremiacoes;
using Campeonato.Infraestrutura.ServicosExternos.GeracaoDocumentoEmPDF;
using Campeonato.Infraestrutura.ServicosExternos.ArmazenamentoEmNuvem;
using Campeonato.Aplicacao.Util;
using Campeonato.Aplicacao.GestaoDeDashboard;
using Campeonato.SendGrid;
using Campeonato.Aplicacao.MontagemDeEmails;
using Campeonato.Aplicacao.GestaoDeTemporadas;

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