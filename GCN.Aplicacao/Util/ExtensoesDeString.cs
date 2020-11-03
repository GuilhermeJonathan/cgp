using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.Util
{
    public static class ExtensoesDeString
    {
        public static string PrimeiraLetraEmMaiusculaDeCadaPalavra(this string palavra)
        {
            if (string.IsNullOrEmpty(palavra))
                return palavra;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(palavra.ToLower());
        }

        public static int ConverterParaInteiro(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return 0;

            return Convert.ToInt32(texto);
        }

        public static decimal ConverterParaDecimal(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return 0;

            return Convert.ToDecimal(texto);
        }

        public static decimal ConverterParaDecimalComVirgula(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return 0;

            texto = texto.Replace(".", ",");

            return Convert.ToDecimal(texto);
        }

        public static string SomenteNumero(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            Regex somenteNumeroRegex = new Regex(@"[^\d]");
            return somenteNumeroRegex.Replace(texto, "");
        }

        public static string FormatarNumeroDeTelefone(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            try
            {
                texto = texto.SomenteNumero();

                string strMascara = "{0:(00)0000-0000}";
                long lngNumero = Convert.ToInt64(texto);

                if (texto.Length == 11)
                    strMascara = "{0:(00)00000-0000}";

                return string.Format(strMascara, lngNumero);
            }
            catch (Exception ex)
            {

            }

            return texto;
        }

        public static string ConfigurarMensagemParaNaoTerCaracteresEstranhos(this string mensagem)
        {
            if (String.IsNullOrEmpty(mensagem))
                return mensagem;

            //byte[] utf8Bytes = Encoding.UTF8.GetBytes(mensagem);
            //byte[] win1252Bytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("Windows-1252"), utf8Bytes);
            //string sConvertedString = Encoding.UTF8.GetString(win1252Bytes);

            return mensagem;
        }

        public static string PreecherAtributosDoEnum(Enum value)

        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();

        }

        public static string RemoverMoeda(this string mensagem)
        {
            return mensagem?.Replace("R$", "");
        }

        public static decimal RetornarDuasCasas(this decimal valor)
        {
            if (valor == 0)
                return valor;

            var formatacao = double.Parse(valor.ToString());

            var calcular = Math.Truncate(100 * formatacao) / 100;

            return decimal.Parse(calcular.ToString());
        }

        public static double RetornarDuasCasas(this double valor)
        {
            if (valor == 0)
                return valor;

            var formatacao = double.Parse(valor.ToString());

            var calcular = Math.Truncate(100 * formatacao) / 100;

            return double.Parse(calcular.ToString());
        }

        public static decimal ConverterParaDecimal(this double valor)
        {
            return Convert.ToDecimal(valor);
        }

        public static double ArrendodamentoEmDouble(this double valor)
        {
            return Math.Round(valor, 2);
        }

        public static string RemoverOqueIniciaComZero(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;



            return valor.TrimStart('0');

        }
    }
}
