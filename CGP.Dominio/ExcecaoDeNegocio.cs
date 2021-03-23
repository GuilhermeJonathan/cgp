using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio
{
    [Serializable]
    public class ExcecaoDeNegocio : Exception
    {
        public ExcecaoDeNegocio() { }
        public ExcecaoDeNegocio(string message) : base(message) { }
    }
}
