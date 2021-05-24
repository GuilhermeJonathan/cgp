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

        public Veiculo(string placa, string marca, string modelo, string ano, string cor, string chassi, string uf)
        {
            this.Placa = Maisculo(placa);
            this.Marca = Maisculo(marca);
            this.Modelo = Maisculo(modelo);
            this.Ano = Maisculo(ano);
            this.Cor = Maisculo(cor);
            this.Chassi = Maisculo(chassi);
            this.Uf = Maisculo(uf);
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
        public Proprietario Proprietario { get; set; }
        public Possuidor Possuidor { get; set; }

        public void AlterarDadosVeiculo(string marca, string modelo, string ano, string cor, string chassi, string uf)
        {
            this.Marca = Maisculo(marca);
            this.Modelo = Maisculo(modelo);
            this.Ano = Maisculo(ano);
            this.Cor = Maisculo(cor);
            this.Chassi = Maisculo(chassi);
            this.Uf = Maisculo(uf);
        }

        private string Maisculo(string parametro)
        {
            var retorno = parametro;
            if (!String.IsNullOrEmpty(parametro))
                retorno = parametro.ToUpper();

            return retorno;
        }

        public void CadastrarProprietario(Proprietario proprietario)
        {
            if (proprietario == null)
                return;

            this.Proprietario = proprietario;
        }

        public void CadastrarPossuidor(Possuidor possuidor)
        {
            if (possuidor == null)
                return;

            this.Possuidor = possuidor;
        }
    }
}
