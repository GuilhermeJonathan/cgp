using Cgp.Aplicacao.Util;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeFuncionario.Modelos
{
    public class ModeloDeCadastroDeFuncionario
    {
        public ModeloDeCadastroDeFuncionario()
        {
            this.PerfisDeFuncionario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeFuncionario>();
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilDeFuncionario PerfilDeFuncionario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeFuncionario { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Pais { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
    }
}
