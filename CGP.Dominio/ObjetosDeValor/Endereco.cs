using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.ObjetosDeValor
{
    public class Endereco
    {
        public Endereco()
        {

        }

        public Endereco(string pais, string uf, string cidade, string bairro, string cep, string logradouro, string numero, string complemento)
        {
            this.Bairro = bairro;
            this.Cep = cep;
            this.Cidade = cidade;
            this.Complemento = complemento;
            this.Logradouro = logradouro;
            this.Numero = numero;

            if (string.IsNullOrEmpty(pais))
                this.Pais = "Brasil";
            else
                this.Pais = pais;

            this.Uf = uf;
        }

        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Pais { get; set; }
        public string Uf { get; set; }

        public static Endereco Vazio => new Endereco("", "", "", "", "", "", "", "");
        public Endereco Maiusculo()
        {
            this.Bairro?.ToUpper();
            this.Cidade?.ToUpper();
            this.Logradouro?.ToUpper();
            this.Pais?.ToUpper();
            this.Uf?.ToUpper();
            this.Numero?.ToUpper();
            this.Complemento?.ToUpper();

            return this;
        }
    }
}
