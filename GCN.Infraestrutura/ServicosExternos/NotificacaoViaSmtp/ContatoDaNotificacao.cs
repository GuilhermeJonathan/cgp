using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.NotificacaoViaSmtp
{
    public class ContatoDaNotificacao
    {
        public ContatoDaNotificacao(string nome, string contato)
        {
            Nome = nome;
            Contato = contato;
        }

        public string Nome { get; set; }
        public string Contato { get; set; }
    }
}
