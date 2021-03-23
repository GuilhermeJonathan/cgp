using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.MontagemDeEmails
{
    public class ModeloDeEmail
    {
        public ModeloDeEmail()
        {

        }

        public ModeloDeEmail(string titulo, string mensagem)
        {
            this.Titulo = titulo;
            this.Mensagem = mensagem;
        }

        public string Titulo { get; }
        public string Mensagem { get; }
    }
}
