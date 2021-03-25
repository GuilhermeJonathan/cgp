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
            this.NomeCidade = carater.Cidade != null ? carater.Cidade.Descricao : String.Empty;
            this.NomeCrime = carater.Crime!= null ? $"{carater.Crime.Nome}" : String.Empty;
            this.DataDoCadastro = carater.DataHoraDoFato != null ? carater.DataHoraDoFato.Value.ToShortDateString() : String.Empty;
            this.SituacaoDoCarater = carater.SituacaoDoCarater.ToString();

            if (carater.Veiculo != null)
            {
                this.NomeVeiculo = carater.Veiculo != null ? $"{carater.Veiculo.Modelo} - {carater.Veiculo.Marca}" : String.Empty;
                this.PlacaVeiculo = carater.Veiculo != null ? $"{carater.Veiculo.Placa} {carater.Veiculo.Uf}" : String.Empty;
                this.CorVeiculo = carater.Veiculo.Cor;
                this.ChassiVeiculo = carater.Veiculo.Chassi;
                this.AnoVeiculo = carater.Veiculo.Ano;
            }
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string NomeCidade { get; set; }
        public string ComplementoEndereco { get; set; }
        public string NomeCrime { get; set; }
        public string NomeVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string ChassiVeiculo { get; set; }
        public string AnoVeiculo { get; set; }
        public string DataDoCadastro { get; set; }
        public string SituacaoDoCarater { get; set; }
    }
}
