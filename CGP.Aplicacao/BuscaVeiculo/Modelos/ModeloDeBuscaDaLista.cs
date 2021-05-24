using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDeBuscaDaLista
    {
        public ModeloDeBuscaDaLista(ModeloDeBuscaCompleto modelo)
        {
            this.Placa = modelo.placa;
            this.MarcaModelo = modelo.marcaModelo;
            this.AnoModelo = $"{modelo.anoFabricacao}/{modelo.anoModelo}";
            this.Cor = modelo.cor;
            this.Municipio = modelo.municipioPlaca;
            this.Renavam = modelo.renavam;
            this.Chassi = modelo.chassi;
            this.Motor = modelo.numeroMotor;
            this.UltimoCRV = !String.IsNullOrEmpty(modelo.dataEmissaoUltimoCRV) ? Convert.ToDateTime(modelo.dataEmissaoUltimoCRV).ToShortDateString() : String.Empty;
            this.Atualizacao = modelo.dataHoraAtualizacaoVeiculo.ToString();
            this.Situacao = modelo.situacaoVeiculo;
            
            if(modelo.proprietario != null)
            {
                this.NomeProprietario = modelo.proprietario.nomeProprietario;
                this.EnderecoProprietario = modelo.proprietario.enderecoProprietario;
                this.DocumentoProprietario = modelo.proprietario.numeroDocumentoProprietario;
            }
            
            if(modelo.possuidor != null)
            {
                this.NomePossuidor = modelo.possuidor.nomePossuidor;
                this.EnderecoPossuidor = modelo.possuidor.enderecoPossuidor;
                this.DocumentoPossuidor = modelo.possuidor.numeroDocumentoPossuidor;
            }

            if(modelo.restricao != null && modelo.restricao.Count > 0)
            {
                this.TemRestricao = true;
                this.Restricao = new ModeloDeRestricaoDaLista(modelo.restricao.FirstOrDefault());
            }
        }

        public string Placa { get; set; }
        public string MarcaModelo { get; set; }
        public string AnoModelo { get; set; }
        public string Cor { get; set; }
        public string Municipio { get; set; }
        public string Renavam { get; set; }
        public string Chassi { get; set; }
        public string Motor { get; set; }
        public string UltimoCRV { get; set; }
        public string Atualizacao { get; set; }
        public string Situacao { get; set; }
        public string NomeProprietario { get; set; }
        public string EnderecoProprietario { get; set; }
        public string DocumentoProprietario { get; set; }
        public string NomePossuidor { get; set; }
        public string EnderecoPossuidor { get; set; }
        public string DocumentoPossuidor { get; set; }
        public bool TemRestricao { get; set; } = false;
        public ModeloDeRestricaoDaLista Restricao { get; set; }

    }
}
