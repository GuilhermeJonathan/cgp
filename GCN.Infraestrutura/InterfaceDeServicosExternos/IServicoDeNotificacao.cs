using Campeonato.Infraestrutura.ServicosExternos.NotificacaoViaSmtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.InterfaceDeServicosExternos
{
    public interface IServicoDeNotificacao
    {
        Task EnviarNotificacao(ContatoDaNotificacao remetente, IEnumerable<ContatoDaNotificacao> destinatarios, string titulo, string mensagem);
        //Task EnviarNotificacao(KeyValuePair<string, string> remetente, IDictionary<string, string> destinatarios, IDictionary<string, string> destinatariosOcultos, string assunto, string mensagem, string respondePara = null);
    }
}
