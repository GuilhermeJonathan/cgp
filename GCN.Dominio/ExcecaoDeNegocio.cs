using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio
{
    [Serializable]
    class ExcecaoDeNegocio : Exception
    {
        public ExcecaoDeNegocio() { }
        public ExcecaoDeNegocio(string message) : base(message) { }
    }
}
