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

        public Veiculo(string placa, string marca, string modelo, string ano, string cor)
        {
            this.Placa = placa;
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Cor = cor;
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

    }
}
