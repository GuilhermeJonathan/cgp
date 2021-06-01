using Cgp.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cgp.Infraestrutura.ServicosExternos.Autenticacao.AutenticacaoViaCookieOwin
{
    public class ServicoExternoDeAutenticacaoViaCookieOwin : IServicoExternoDeAutenticacao
    {
        private const string TipoDeAutenticacao = "ApplicationCookie";

        public void Acessar(IDictionary<string, object> informacoesDoUsuario)
        {
            var contextoOwin = HttpContext.Current.GetOwinContext().Authentication;
            var claims = this.GerarClaims(informacoesDoUsuario);
            HttpContext.Current.GetOwinContext().Set("claims", claims);

            var props = new AuthenticationProperties()
            {
                IsPersistent = true
            };

            contextoOwin.SignIn(props, new ClaimsIdentity(claims, TipoDeAutenticacao));
        }

        public string PegarPerfilDoUsuarioLogado()
        {
            var contextoOwin = HttpContext.Current.GetOwinContext().Authentication;
            var claim = contextoOwin.User.Claims.FirstOrDefault(c => c.Type == "perfil".ToLower());
            return claim != null ? claim.Value : string.Empty;
        }

        public void Sair()
        {
            var contextoOwin = HttpContext.Current.GetOwinContext().Authentication;
            contextoOwin.SignOut(TipoDeAutenticacao);
        }

        private IEnumerable<Claim> GerarClaims(IDictionary<string, object> informacoesDoUsuario)
        {
            var claims = new List<Claim>();

            foreach (var informacao in informacoesDoUsuario)
            {
                var chave = informacao.Key.ToLower();
                var valor = informacao.Value?.ToString();

                if (chave == "id")
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, valor));
                else if (chave == "nome")
                    claims.Add(new Claim(ClaimTypes.Name, valor));
                else if (chave == "email" || chave == "login")
                    claims.Add(new Claim(ClaimTypes.Email, valor ?? ""));
                else
                    claims.Add(new Claim(chave, valor ?? ""));
            }

            return claims;
        }
    }
}
