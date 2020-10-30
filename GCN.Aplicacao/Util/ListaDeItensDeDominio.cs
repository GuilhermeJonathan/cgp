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

        public static IEnumerable<SelectListItem> DaClasseComOpcaoPadrao<T>(string texto, string valor, Func<IEnumerable<T>> metodoDeBuscaDaLista, int valorSelecionado = 0) where T : class
        {
            var lista = new List<SelectListItem> { new SelectListItem { Text = "Selecione", Value = string.Empty } };
            var listaDeObjetosRetornados = metodoDeBuscaDaLista.Invoke();
            lista.AddRange(PreencherSelectList<T>(listaDeObjetosRetornados, texto, valor, valorSelecionado));

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
        private static IEnumerable<SelectListItem> PreencherSelectList<T>(IEnumerable<T> lista, string texto, string valor, int valorSelecionado) where T : class
        {
            var listaDeRetorno = new List<SelectListItem>();

            if (lista == null || !lista.Any())
                return listaDeRetorno;

            foreach (var item in lista)
            {
                var tipoDoItem = item.GetType();
                var textoDoItem = tipoDoItem.GetProperty(texto);
                var valorDoItem = tipoDoItem.GetProperty(valor);
                var selecionado = valorDoItem?.GetValue(item).ToString() == valorSelecionado.ToString();

                listaDeRetorno.Add(new SelectListItem
                {
                    Text = textoDoItem?.GetValue(item).ToString(),
                    Value = valorDoItem?.GetValue(item).ToString(),
                    Selected = selecionado
                });
            }

            return listaDeRetorno;
        }

    }
}
