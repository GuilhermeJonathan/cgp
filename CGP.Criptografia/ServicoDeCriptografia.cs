using Cgp.Aplicacao.Criptografia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Criptografia
{
    public class ServicoDeCriptografia : IServicoDeCriptografia
    {
        public string Encriptar(string valor)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(valor));
        }

        public string Decriptar(string valor)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(valor));
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Não foi possível decriptar o valor informado.");
            }
        }
    }
}
