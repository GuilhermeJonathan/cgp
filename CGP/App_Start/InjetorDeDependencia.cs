using Cgp.Aplicacao.Comum;
using Cgp.Aplicacao.GestaoDeDashboard;
using Cgp.Aplicacao.GestaoDeFuncionarios;
using Cgp.Aplicacao.GestaoDeBatalhoes;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.Login;
using Cgp.Aplicacao.MontagemDeEmails;
using Cgp.Aplicacao.Util;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.ArmazenamentoEmNuvem;
using Cgp.Infraestrutura.ServicosExternos.Autenticacao.AutenticacaoViaCookieOwin;
using Cgp.Infraestrutura.ServicosExternos.GeracaoDocumentoEmPDF;
using Cgp.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using Cgp.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework;
using Cgp.SendGrid;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using Container = SimpleInjector.Container;
using Cgp.Aplicacao.GestaoDeComandosRegionais;
using Cgp.Aplicacao.GestaoDeCidades;
using Cgp.Aplicacao.GestaoDeVeiculos;
using Cgp.ComunicacaoViaHttp;
using Cgp.Aplicacao.ComunicacaoViaHttp;
using Cgp.Aplicacao.BuscaVeiculo;
using Cgp.Aplicacao.GestaoDeCrimes;

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
            container.Register<IServicoDeGestaoDeBatalhoes, ServicoDeGestaoDeBatalhoes>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeUsuarios, ServicoDeGestaoDeUsuarios>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeDashboard, ServicoDeGestaoDeDashboard>(Lifestyle.Scoped);
            container.Register<IServicoDeGeracaoDeDocumentosEmPdf, ServicoDeGeracaoDeDocumentosEmPdf>(Lifestyle.Scoped);
            container.Register<IServicoDeEnvioDeEmails, ServicoDeEnvioDeEmails>(Lifestyle.Scoped);
            container.Register<IServicoDeMontagemDeEmails, ServicoDeMontagemDeEmails>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeComandosRegionais, ServicoDeGestaoDeComandosRegionais>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeCidades, ServicoDeGestaoDeCidades>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeVeiculos, ServicoDeGestaoDeVeiculos>(Lifestyle.Scoped);
            container.Register<IServicoDeGestaoDeCrimes, ServicoDeGestaoDeCrimes>(Lifestyle.Scoped);

            container.Register<IServicoExternoDeArmazenamentoEmNuvem>(() => new ServicoExternoDeArmazenamentoEmNuvem(
               VariaveisDeAmbiente.Pegar<string>("azure:contaDeArmazenamentoAzure"), VariaveisDeAmbiente.Pegar<string>("azure:chaveDaContaDeArmazenamentoAzure")));

            container.Register<IServicoDeComunicacaoViaHttp, ServicoDeComunicacaoViaHttp>(Lifestyle.Scoped);

            container.Register<IServicoDeBuscaDeVeiculo>(() => new ServicoDeBuscaDeVeiculo(container.GetInstance<IServicoDeComunicacaoViaHttp>(), VariaveisDeAmbiente.Pegar<string>("apiBuscaVeiculoSimples")), Lifestyle.Scoped);

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}