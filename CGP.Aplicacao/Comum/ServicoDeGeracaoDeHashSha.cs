using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Comum
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

        public string GerarParaStream(Stream s)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream arquivo = new MemoryStream())
            {
                int read;
                while ((read = s.Read(buffer, 0, buffer.Length)) > 0)
                {
                    arquivo.Write(buffer, 0, read);
                }

                MD5 alg = MD5.Create();

                arquivo.Position = 0;
                byte[] byteImagem = arquivo.ToArray();

                byte[] bt = alg.ComputeHash(byteImagem);
                StringBuilder sb = new System.Text.StringBuilder();
                foreach (byte aa in bt)
                    sb.Append(aa.ToString("x2").ToLower());

                return sb.ToString();
            }
        }

        public string EncodeToBase64(string texto)
        {
            try
            {
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DecodeFrom64(string dados)
        {
            try
            {
                byte[] dadosAsBytes = System.Convert.FromBase64String(dados);
                string resultado = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
