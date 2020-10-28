using GCN.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GCN.Infraestrutura.ServicosExternos.Autenticacao.AutenticacaoViaCookieOwin
{
    public class ServicoExternoDeAutenticacaoViaCookieOwin : IServicoExternoDeAutenticacao
    {
        private const string TipoDeAutenticacao = "ApplicationCookie";

        public void Acessar(IDictionary<string, object> informacoesDoUsuario)
        {
            var contextoOwin = HttpContext.Current.GetOwinContext().Authentication;
            contextoOwin.SignIn(new ClaimsIdentity(this.GerarClaims(informacoesDoUsuario), TipoDeAutenticacao));
        }

        public string PegarPerfilDoUsuarioLogado()
        {
            var contextoOwin = HttpContext.Current.GetOwinContext().Authentication;
            var claim = contextoOwin.User.Claims.FirstOrDefault(c => c.Type == "perfil".ToLower());

            return claim != null ? claim.Value : string.Empty;
        }

        public int PegarEmpresaDoUsuarioLogado()
        {
            var contextoOwin = HttpContext.Current.GetOwinContext().Authentication;
            var claim = contextoOwin.User.Claims.FirstOrDefault(c => c.Type == "empresa".ToLower());

            return claim != null ? int.Parse(claim.Value) : 0;
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
                else if (chave == "email")
                    claims.Add(new Claim(ClaimTypes.Email, valor));
                else
                    claims.Add(new Claim(chave, valor ?? ""));
            }

            return claims;
        }
    }
}
