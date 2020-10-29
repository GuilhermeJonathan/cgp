using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Campeonato.Web.Startup))]

namespace Campeonato.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var configuracaoDoTempoDeSessao = "30";
            var sessaoEmMinutos = !string.IsNullOrEmpty(configuracaoDoTempoDeSessao) ? double.Parse(configuracaoDoTempoDeSessao) : 90;

            var configuracaoDaPaginaDeLogin = "/Login";
            var paginaDeLogin = !string.IsNullOrEmpty(configuracaoDaPaginaDeLogin) ? configuracaoDaPaginaDeLogin : "/Login";

            var configuracaoDoDominio = "http://localhost:65427";

            var opcoes = new CookieAuthenticationOptions();
            opcoes.AuthenticationType = "ApplicationCookie";
            opcoes.SlidingExpiration = true;
            opcoes.CookieHttpOnly = true;
            opcoes.LoginPath = new PathString(paginaDeLogin);
            opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(sessaoEmMinutos);
            opcoes.CookieSecure = CookieSecureOption.SameAsRequest;
            opcoes.CookieDomain = new Uri(configuracaoDoDominio).Host;

            opcoes.CookieName = "campeonato_secure";

            app.UseCookieAuthentication(opcoes);
        }
    }
}
