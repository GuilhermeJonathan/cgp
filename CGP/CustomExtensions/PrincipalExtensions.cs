using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Cgp.Web.CustomExtensions
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

        public static string NomeCompleto(this IPrincipal principal)
        {
            return Claim(principal, "nomeCompleto");
        }

        public static string Lotacao(this IPrincipal principal)
        {
            return Claim(principal, "lotacao");
        }

        public static string Email(this IPrincipal principal)
        {
            return Claim(principal, ClaimTypes.Email);
        }

        public static string Cpf(this IPrincipal principal)
        {
            return Claim(principal, "cpf");
        }

        public static string Matricula(this IPrincipal principal)
        {
            return Claim(principal, "matricula");
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

        public static bool EhInterno(this IPrincipal principal)
        {
            var perfil = Claim(principal, "perfil");
            if ((PerfilDeUsuario)Enum.Parse(typeof(PerfilDeUsuario), perfil) == PerfilDeUsuario.Interno)
                return true;
            else return false;
        }

        public static bool EhUsuario(this IPrincipal principal)
        {
            var perfil = Claim(principal, "perfil");
            if ((PerfilDeUsuario)Enum.Parse(typeof(PerfilDeUsuario), perfil) == PerfilDeUsuario.Usuario)
                return true;
            else return false;
        }

        public static bool EhAtenas(this IPrincipal principal)
        {
            var perfil = Claim(principal, "perfil");
            if ((PerfilDeUsuario)Enum.Parse(typeof(PerfilDeUsuario), perfil) == PerfilDeUsuario.Atenas)
                return true;
            else return false;
        }


        public static UsuarioLogado Logado(this IPrincipal principal)
        {
            return new UsuarioLogado(principal.Id(), principal.Nome(), principal.NomeCompleto(), principal.Email(), 
                principal.Perfil(), principal.Cpf(), principal.Matricula(), principal.Lotacao());
        }

        private static string Claim(IPrincipal usuario, string chave)
        {
            var claims = ((ClaimsIdentity)usuario.Identity).Claims;
            var claim = claims.FirstOrDefault(c => c.Type == chave.ToLower());

            return claim != null ? claim.Value : string.Empty;
        }
    }
}