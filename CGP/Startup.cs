using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Cgp.Aplicacao.Util;
using System.Configuration;

[assembly: OwinStartup(typeof(Cgp.Startup))]

namespace Cgp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuracaoDoTempoDeSessao = "60";
            var sessaoEmMinutos = !string.IsNullOrEmpty(configuracaoDoTempoDeSessao) ? double.Parse(configuracaoDoTempoDeSessao) : 90;

            var configuracaoDaPaginaDeLogin = "/Login";
            var paginaDeLogin = !string.IsNullOrEmpty(configuracaoDaPaginaDeLogin) ? configuracaoDaPaginaDeLogin : "/Login";

            var configuracaoDoDominio = VariaveisDeAmbiente.Pegar<string>("NomeDoSite");

            var opcoes = new CookieAuthenticationOptions();
            opcoes.AuthenticationType = "ApplicationCookie";
            opcoes.SlidingExpiration = true;
            opcoes.CookieHttpOnly = true;
            opcoes.LoginPath = new PathString(paginaDeLogin);
            opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(sessaoEmMinutos);
            opcoes.CookieSecure = CookieSecureOption.SameAsRequest;
            opcoes.CookieDomain = new Uri(configuracaoDoDominio).Host;

            opcoes.CookieName = VariaveisDeAmbiente.Pegar<string>("NomeDoSite") + "_secure";

            app.UseCookieAuthentication(opcoes);
        }
    }
}
