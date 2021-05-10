using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class Senha
    {
        public const int TamanhoMinimo = 6;
        public const int TamanhoMaximo = 30;

        public Senha()
        {
        }

        public Senha(string valor, Func<string, string> gerarHash = null)
        {
            if (string.IsNullOrEmpty(valor))
                throw new ExcecaoDeNegocio("A senha não pode ser vazia");

            if (valor.Length < TamanhoMinimo || valor.Length > TamanhoMaximo)
                throw new ExcecaoDeNegocio($"A senha deve ter entre {TamanhoMinimo} e {TamanhoMaximo} digitos");

            this.ValorOriginal = valor;
            this.Valor = gerarHash != null ? GerarHash(valor) : valor;
        }

        public string Valor { get; private set; }
        public string ValorOriginal { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var senha = (Senha)obj;

            return senha.Valor == this.Valor;
        }

        public override int GetHashCode()
        {
            return this.Valor.GetHashCode();
        }

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
        public static Senha Gerar(Func<string, string> gerarHash = null)
        {
            var senha = new StringBuilder();
            var randomizador = new Random();

            senha.Append(Convert.ToChar(randomizador.Next(64, 90)));
            senha.Append(Convert.ToChar(randomizador.Next(97, 122)));
            senha.Append(Convert.ToChar(randomizador.Next(48, 57)));
            senha.Append(Convert.ToChar(randomizador.Next(48, 57)));
            senha.Append(Convert.ToChar(randomizador.Next(64, 90)));
            senha.Append(Convert.ToChar(randomizador.Next(48, 57)));

            var senhaFormatada = senha.ToString().ToLower();

            return new Senha(senhaFormatada, gerarHash);
        }
    }
}
