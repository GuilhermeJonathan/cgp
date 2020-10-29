using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.Comum
{
    [Serializable]
    public class ExcecaoDeAplicacao : Exception
    {
        public ExcecaoDeAplicacao() { }
        public ExcecaoDeAplicacao(string message) : base(message) { }
        public ExcecaoDeAplicacao(string message, Exception inner) : base(message, inner) { }
        protected ExcecaoDeAplicacao(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
