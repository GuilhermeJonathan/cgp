using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Aplicacao.Comum
{
    public class ServicoDeGeracaoDeHashSha : IServicoDeGeracaoDeHashSha
    {
        public string GerarHash(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            var sha1 = SHA1.Create();

            var bytes = Encoding.UTF8.GetBytes(valor);
            var hash = sha1.ComputeHash(bytes);

            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
