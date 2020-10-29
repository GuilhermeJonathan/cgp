using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.Util
{
    public static class ListaDeItensDeDominio
    {
        public static IEnumerable<SelectListItem> DoEnumComOpcaoPadrao<T>(string defaultText = "Selecione") where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException("Não é possível criar uma lista de SelectListItens de um tipo que não é Enum");

            var lista = new List<SelectListItem>
            {
                new SelectListItem { Text = defaultText, Value = string.Empty }
            };

            foreach (var item in Enum.GetValues(typeof(T)))
            {

                lista.Add(new SelectListItem { Text = preecherAtributosDoEnum((Enum)Enum.Parse(typeof(T), item.ToString())), Value = ((int)item).ToString() });
            }

            return lista;
        }

        public static string preecherAtributosDoEnum(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();

        }
    }
}
