using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.ModelosCortex
{
    public class ModeloDeBuscaCompleto
    {
        public ModeloDeBuscaCompleto()
        {

        }

        public string alarme { get; set; }
        public string anoFabricacao { get; set; }
        public string anoModelo { get; set; }
        public Arrendatario arrendatario { get; set; }
        public string capacidadeMaximaCarga { get; set; }
        public string capacidadeMaximaTracao { get; set; }
        public string carroceria { get; set; }
        public string categoria { get; set; }
        public string chassi { get; set; }
        public string cilindrada { get; set; }
        public string codigoMunicipioEmplacamento { get; set; }
        public string codigoOrgaoSRF { get; set; }
        public string combustivel { get; set; }
        public string cor { get; set; }
        public DateTime dataAtualizacaoAlarme { get; set; }
        public DateTime dataAtualizacaoRouboFurto { get; set; }
        public DateTime dataAtualizacaoVeiculo { get; set; }
        public DateTime dataDeclaracaoImportacao { get; set; }
        public string dataEmissaoUltimoCRV { get; set; }
        public DateTime dataEmplacamento { get; set; }
        public DateTime dataHoraAtualizacaoVeiculo { get; set; }
        public DateTime dataLimiteRestricaoTributaria { get; set; }
        public string especie { get; set; }
        public string grupoVeiculo { get; set; }
        public string indicadorRemarcacaoChassi { get; set; }
        public string indicadorVeiculoLicenciadoCirculacao { get; set; }
        public bool indicadorVeiculoNacional { get; set; }
        public string lotacao { get; set; }
        public string marcaModelo { get; set; }
        public string municipioPlaca { get; set; }
        public string nomeArrendatario { get; set; }
        public string nomePossuidor { get; set; }
        public string nomeProprietario { get; set; }
        public string numeroCaixaCambio { get; set; }
        public string numeroCarroceria { get; set; }
        public string numeroDeclaracaoImportacao { get; set; }
        public string numeroEixoAuxiliar { get; set; }
        public string numeroEixoTraseiro { get; set; }
        public string numeroIdentificacaoFaturado { get; set; }
        public string numeroIdentificacaoImportador { get; set; }
        public string numeroLicencaUsoConfiguracaoVeiculosMotor { get; set; }
        public string numeroMotor { get; set; }
        public string numeroProcessoImportacao { get; set; }
        public string origemPossuidor { get; set; }
        public string paisTransferenciaVeiculo { get; set; }
        public string pesoBrutoTotal { get; set; }
        public string placa { get; set; }
        public Possuidor possuidor { get; set; }
        public string potencia { get; set; }
        public Proprietario proprietario { get; set; }
        public string quaantidadeRestricoesBaseEmplacamento { get; set; }
        public string quantidadeEixo { get; set; }
        public string registroAduaneiro { get; set; }
        public string renavam { get; set; }
        public List<Restricao> restricao { get; set; }
        public string restricaoVeiculo1 { get; set; }
        public string restricaoVeiculo2 { get; set; }
        public string restricaoVeiculo3 { get; set; }
        public string restricaoVeiculo4 { get; set; }
        public string rouboFurto { get; set; }
        public string situacaoVeiculo { get; set; }
        public string tipoDocumentoFaturado { get; set; }
        public string tipoDocumentoProprietario { get; set; }
        public string tipoMontagem { get; set; }
        public string tipoVeiculo { get; set; }
        public string ufEmplacamento { get; set; }
        public string ufFatura { get; set; }
        public string chassiTratado => !String.IsNullOrEmpty(this.chassi) ? this.chassi.Substring(this.chassi.Length - 5, 5) : String.Empty;

    }
}
