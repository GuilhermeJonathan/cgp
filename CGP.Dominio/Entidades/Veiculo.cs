using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Veiculo : Entidade
    {
        public Veiculo()
        {

        }

        public Veiculo(string placa, string marca, string modelo, string ano, string cor, string chassi)
        {
            this.Placa = Maisculo(placa);
            this.Marca = Maisculo(marca);
            this.Modelo = Maisculo(modelo);
            this.Ano = Maisculo(ano);
            this.Cor = Maisculo(cor);
            this.Chassi = Maisculo(chassi);
        }

        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Chassi { get; set; }
        public string Cor { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Situacao { get; set; }

        private string Maisculo(string parametro)
        {
            var retorno = parametro;
            if (!String.IsNullOrEmpty(parametro))
                retorno = parametro.ToUpper();

            return retorno;
        }
    }
}
