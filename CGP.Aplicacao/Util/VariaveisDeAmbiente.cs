using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Util
{
    public static class VariaveisDeAmbiente
    {
        public static string ConnectionString(string nomeDaConnectionString)
        {
            var valor = ConfigurationManager.ConnectionStrings[nomeDaConnectionString];

            if (valor == null)
                throw new InvalidOperationException($"A connection string {nomeDaConnectionString} não foi encontrada. Verifique seu arquivo de configurações.");

            return valor.ToString();
        }

        public static T Pegar<T>(string nomeDaVarivavel, bool opcional = false)
        {
            var valor = ConfigurationManager.AppSettings[nomeDaVarivavel];

            if (valor == null && !opcional)
            {
                if (typeof(T) == typeof(bool))
                    return (T)Convert.ChangeType(false, typeof(T));
                else
                    throw new InvalidOperationException($"A chave {nomeDaVarivavel} não foi encontrada. Verifique seu arquivo de configurações.");
            }

            if (valor == null && opcional)
            {
                if (typeof(T) == typeof(int) || typeof(T) == typeof(float) || typeof(T) == typeof(decimal) || typeof(T) == typeof(double)
                || typeof(T) == typeof(short) || typeof(T) == typeof(byte))
                    valor = "0";
                else if (typeof(T) == typeof(bool))
                    valor = "false";
                else if (typeof(T) == typeof(DateTime))
                    valor = DateTime.MinValue.ToString();
                else
                    valor = "";
            }

            try
            {
                return (T)Convert.ChangeType(valor, typeof(T));
            }
            catch (InvalidCastException)
            {
                throw new InvalidOperationException($"Não foi possível converter o valor da chave {nomeDaVarivavel} para o tipo solicitado. Verifique o valor da chave e o tipo de conversão.");
            }
            catch (FormatException)
            {
                throw new InvalidOperationException($"Não foi possível converter o valor da chave {nomeDaVarivavel} para o tipo solicitado. Verifique o valor da chave e o tipo de conversão.");
            }
            catch (OverflowException)
            {
                throw new InvalidOperationException($"Não foi possível converter o valor da chave {nomeDaVarivavel} para o tipo solicitado. Verifique se o valor da chave se adequa ao tipo de conversão.");
            }
        }
    }
}
