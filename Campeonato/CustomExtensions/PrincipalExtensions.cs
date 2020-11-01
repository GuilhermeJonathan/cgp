using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Campeonato.Web.CustomExtensions
{
    public static class PrincipalExtensions
    {
        public static int Id(this IPrincipal principal)
        {
            return int.Parse(Claim(principal, ClaimTypes.NameIdentifier));
        }

        public static string Nome(this IPrincipal principal)
        {
            return Claim(principal, ClaimTypes.Name);
        }

        public static string Email(this IPrincipal principal)
        {
            return Claim(principal, ClaimTypes.Email);
        }

        public static PerfilDeUsuario Perfil(this IPrincipal principal)
        {
            var perfil = Claim(principal, "perfil");

            return !string.IsNullOrEmpty(perfil) ? (PerfilDeUsuario)Enum.Parse(typeof(PerfilDeUsuario), perfil) : default(PerfilDeUsuario);
        }

        public static DateTime DataDoCadastro(this IPrincipal principal)
        {
            var claim = Claim(principal, "dataDoCadastro");
            return !string.IsNullOrEmpty(claim) ? DateTime.Parse(claim) : DateTime.MinValue;
        }

        public static bool Autenticado(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        public static bool EhAdministrador(this IPrincipal principal)
        {
            var perfil = Claim(principal, "perfil");
            if ((PerfilDeUsuario)Enum.Parse(typeof(PerfilDeUsuario), perfil) == PerfilDeUsuario.Administrador)
                return true;
            else return false;
        }

        public static UsuarioLogado Logado(this IPrincipal principal)
        {
            return new UsuarioLogado(principal.Id(), principal.Nome(), principal.Email(), principal.Perfil());
        }

        private static string Claim(IPrincipal usuario, string chave)
        {
            var claims = ((ClaimsIdentity)usuario.Identity).Claims;
            var claim = claims.FirstOrDefault(c => c.Type == chave.ToLower());

            return claim != null ? claim.Value : string.Empty;
        }
    }
}