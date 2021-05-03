using Cgp.Aplicacao.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuariosSGPOL.Modelos
{
    public class ModeloDeAutorizacaoSGPOL
    {
        public ModeloDeAutorizacaoSGPOL()
        {
            this.Username = VariaveisDeAmbiente.Pegar<string>("SGPOL:username");
            this.Password = VariaveisDeAmbiente.Pegar<string>("SGPOL:password");  
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string UserPass => $"{this.Username}:{this.Password}";
        public string TokenCriptografado(Func<string, string> funcaoDeCriptografia) => funcaoDeCriptografia.Invoke($"{this.Username}:{this.Password}");
    }
}
