using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeCaratersDaLista
    {
        public ModeloDeCaratersDaLista()
        {

        }

        public ModeloDeCaratersDaLista(Carater carater)
        {
            if (carater == null)
                return;

            this.Id = carater.Id;
            this.Descricao = carater.Descricao;
            this.ComplementoEndereco = carater.ComplementoEndereco;
            this.ComplementoEnderecoTradado = !String.IsNullOrEmpty(carater.ComplementoEndereco) ? $" - {carater.ComplementoEndereco}" : String.Empty;

            this.NomeCidade = carater.Cidade != null ? carater.Cidade.Descricao : String.Empty;
            this.NomeCidadeAbreviada = carater.Cidade != null ? carater.Cidade.Sigla : String.Empty;
            this.NomeCrime = carater.Crime!= null ? $"{carater.Crime.Nome}" : String.Empty;
            this.DataDoFato = carater.DataHoraDoFato != null ? carater.DataHoraDoFato.Value.ToString("dd/MM/yyyy HH:mm") : String.Empty;
            this.SituacaoDoCarater = carater.SituacaoDoCarater.ToString();
            this.VeiculoLocalizado = carater.SituacaoDoCarater == Dominio.ObjetosDeValor.SituacaoDoCarater.Localizado ? true : false;
            this.CssTipoCrime = RetornaCssCrime(NomeCrime);

            if (carater.Veiculo != null)
            {
                var chassi = !String.IsNullOrEmpty(carater.Veiculo.Chassi) ? $"({carater.Veiculo.Chassi})" : String.Empty;
                this.NomeVeiculo = carater.Veiculo != null ? $"{carater.Veiculo.Marca} {carater.Veiculo.Modelo} {chassi}" : String.Empty;
                this.PlacaVeiculo = carater.Veiculo != null ? $"{carater.Veiculo.Placa} {carater.Veiculo.Uf}" : String.Empty;
                this.PlacaInicial = carater.Veiculo != null ? $"{carater.Veiculo.Placa.Substring(3,4)}" : String.Empty;
                this.PlacaFinal = carater.Veiculo != null ? $"{carater.Veiculo.Placa.Substring(0,3)}" : String.Empty;
                this.CorVeiculo = carater.Veiculo.Cor;
                this.UfVeiculo = !String.IsNullOrEmpty(carater.Veiculo.Uf) ? carater.Veiculo.Uf : String.Empty;
                this.ChassiVeiculo = carater.Veiculo.Chassi;
                this.AnoVeiculo = !String.IsNullOrEmpty(carater.Veiculo.Ano) ? carater.Veiculo.Ano.Split('/')[0].ToString() : String.Empty;
            }
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string NomeCidade { get; set; }
        public string NomeCidadeAbreviada { get; set; }
        public string ComplementoEndereco { get; set; }
        public string ComplementoEnderecoTradado { get; set; }
        public string NomeCrime { get; set; }
        public string NomeVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
        public string PlacaInicial { get; set; }
        public string PlacaFinal { get; set; }
        public string CorVeiculo { get; set; }
        public string ChassiVeiculo { get; set; }
        public string AnoVeiculo { get; set; }
        public string UfVeiculo { get; set; }
        public string DataDoFato { get; set; }
        public string SituacaoDoCarater { get; set; }
        public string CssTipoCrime { get; set; }
        public bool VeiculoLocalizado { get; set; }

        private string RetornaCssCrime(string crime)
        {
            var retorno = String.Empty;

            switch (crime.ToUpper())
            {
                case "ROUBO":
                    retorno = "badge bg-danger";
                    break;
                case "FURTO":
                    retorno = "badge bg-warning";
                    break;
                default:
                    retorno = "badge bg-primary";
                    break;
            }

            return retorno;
        }
    }
}
