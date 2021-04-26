using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;

namespace Cgp.Aplicacao.Login
{
    public class ServicoDeLoginAd : IServicoDeLoginAd
    {
        private readonly string _servidor;
        private readonly string _dominio;
        
        public ServicoDeLoginAd(string servidor)
        {
            if (string.IsNullOrEmpty(servidor))
                throw new InvalidOperationException("Não é possível criar um serviço de Login AD");

            this._servidor = servidor;
            this._dominio = "PMDF";
        }

        public Usuario Autenticar(string userName, string password)
        {
            try
            {
                var domain = $"{_dominio}\\{userName}";
                var entry = new DirectoryEntry(this._servidor, domain, password);
                var search = new DirectorySearcher(entry);

                search.Filter = String.Format("(SamAccountName={0})", userName);
                search.PropertiesToLoad.Add("EmployeeID");

                SearchResult resultado = search.FindOne();
                var usuario = new Usuario();

                if (resultado == null)
                    return usuario;

                var matricula = resultado.Properties["EmployeeID"][0].ToString();
                var nome = resultado.Properties["adspath"][0].ToString();
                usuario.Matricula = !String.IsNullOrEmpty(matricula) ? matricula : String.Empty;
                usuario.Nome = new Nome(nome);
                return usuario;
            } catch(Exception ex)
            {
                return null;
            }
        }
    }
}
