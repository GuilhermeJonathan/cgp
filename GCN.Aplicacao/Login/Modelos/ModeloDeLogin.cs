using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.Login.Modelos
{
    public class ModeloDeLogin
    {
        public ModeloDeLogin()
        {

        }

        public ModeloDeLogin(string login, string senha, string ip) : this()
        {
            this.Login = login;
            this.Senha = senha;
            this.Ip = ip;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public string Ip { get; set; }

        public string HashDaSenha(Func<string, string> funcaoDeGeracaoDeHash)
        {
            if (funcaoDeGeracaoDeHash == null)
                return "";

            return funcaoDeGeracaoDeHash.Invoke(this.Senha);
        }

        public string SenhaCriptograda(Func<string, string> funcaoDeCriptografia) => funcaoDeCriptografia.Invoke(this.Senha);
    }

}
