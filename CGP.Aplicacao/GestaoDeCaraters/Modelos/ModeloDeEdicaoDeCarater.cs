using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeEdicaoDeCarater
    {
        public ModeloDeEdicaoDeCarater()
        {
            this.Crimes = new List<SelectListItem>();
            this.Cidades = new List<SelectListItem>();
            this.SituacoesDoCarater = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<SituacaoDoCarater>();
        }

        public ModeloDeEdicaoDeCarater(Carater carater)
        {
            if (carater == null)
                return;

            var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");
            this.Id = carater.Id;
            this.Descricao = carater.Descricao;
            this.ComplementoEndereco = carater.ComplementoEndereco;

            this.Cidade = carater.Cidade != null ? carater.Cidade.Id : 0;
            this.Crime = carater.Crime != null ? carater.Crime.Id : 0;
            
            this.Data = carater.DataDoCadastro.ToShortDateString();
            this.Hora = carater.DataDoCadastro.ToShortTimeString();
            this.UrlImagem = $"{caminhoBlob}/{carater.UrlImagem}";

            if(carater.Veiculo != null)
            {
                this.Placa = carater.Veiculo.Placa;
                this.IdVeiculo = carater.Veiculo.Id;
                this.ModeloVeiculo = carater.Veiculo.Modelo;
                this.MarcaVeiculo = carater.Veiculo.Marca;
                this.AnoVeiculo = carater.Veiculo.Ano;
                this.CorVeiculo = carater.Veiculo.Cor;
            }
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string ComplementoEndereco { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public int Crime { get; set; }
        public IEnumerable<SelectListItem> Crimes { get; set; }
        public int Veiculo { get; set; }
        public string UrlImagem { get; set; }
        public SituacaoDoCarater SituacaoDoCarater { get; set; }
        public IEnumerable<SelectListItem> SituacoesDoCarater { get; set; }
        public int IdVeiculo { get; set; }
        public string Placa { get; set; }
        public string ModeloVeiculo { get; set; }
        public string MarcaVeiculo { get; set; }
        public string AnoVeiculo { get; set; }
        public string CorVeiculo { get; set; }
    }
}
