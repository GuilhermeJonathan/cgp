using Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos;
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
            this.CidadesLocalizacao = new List<SelectListItem>();
            this.SituacoesDoCarater = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<SituacaoDoCarater>();
            this.HistoricosDeCaraters = new List<ModeloDeHistoricoDeCaraterDaLista>();
            this.Fotos = new List<ModeloDeFotosDaLista>();
            this.DataHistorico = DateTime.Now.ToShortDateString();
            //this.HoraHistorico = DateTime.Now.ToShortTimeString();
        }

        public ModeloDeEdicaoDeCarater(Carater carater, bool ehCelular)
        {
            if (carater == null)
                return;
            
            this.DataHistorico = DateTime.Now.ToShortDateString();
            
            var situacoesBaixas = new Dominio.ObjetosDeValor.SituacaoDoCarater[] { Dominio.ObjetosDeValor.SituacaoDoCarater.Localizado, Dominio.ObjetosDeValor.SituacaoDoCarater.BaixaAutomatica };

            this.HistoricosDeCaraters = new List<ModeloDeHistoricoDeCaraterDaLista>();
            this.HistoricosDePassagens = new List<ModeloDeHistoricoDePassagensDaLista>();
            this.Fotos = new List<ModeloDeFotosDaLista>();

            this.SituacoesDoCarater = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<SituacaoDoCarater>();

            var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");
            this.Id = carater.Id;
            this.Descricao = carater.Descricao;
            this.ComplementoEndereco = carater.ComplementoEndereco;
            this.ComplementoEnderecoTradado = !String.IsNullOrEmpty(carater.ComplementoEndereco) ? $" - {carater.ComplementoEndereco}" : String.Empty ;

            this.Cidade = carater.Cidade != null ? carater.Cidade.Id : 0;
            this.Crime = carater.Crime != null ? carater.Crime.Id : 0;
            this.NomeCrime = carater.Crime != null ? $"{carater.Crime.Nome}" : String.Empty;
            this.NomeCidade = carater.Cidade != null ? $"{carater.Cidade.Descricao}" : String.Empty;

            this.Data = carater.DataHoraDoFato.HasValue ? carater.DataHoraDoFato.Value.ToShortDateString() : String.Empty;
            this.Hora = carater.DataHoraDoFato.HasValue ? carater.DataHoraDoFato.Value.ToShortTimeString() : String.Empty;
            this.UrlImagem = $"{caminhoBlob}/{carater.UrlImagem}";
            var usuario = carater.UsuarioQueAlterou != null ? carater.UsuarioQueAlterou.PerfilDeUsuario == PerfilDeUsuario.Atenas ?
                carater.UsuarioQueAlterou.PerfilDeUsuario.ToString() : carater.UsuarioQueAlterou.Nome.Valor : String.Empty;
            this.UsuarioCadastro = $"Cadastro por {usuario} no dia {carater.DataDoCadastro.ToString("dd/MM")} às {carater.DataDoCadastro.ToString("HH:mm")}";
            this.SituacaoDoCarater = (int)carater.SituacaoDoCarater;
            this.CssTipoCrime = RetornaCssCrime(NomeCrime);

            if (carater.Veiculo != null)
            {
                this.Placa = carater.Veiculo.Placa;
                this.UfVeiculo = carater.Veiculo.Uf;
                this.MunicipioVeiculo = carater.Veiculo.Municipio;
                this.IdVeiculo = carater.Veiculo.Id;
                this.ModeloVeiculo = carater.Veiculo.Modelo;
                this.MarcaVeiculo = carater.Veiculo.Marca;
                this.AnoVeiculo = carater.Veiculo.Ano;
                this.CorVeiculo = carater.Veiculo.Cor;
                this.ChassiVeiculo = carater.Veiculo.Chassi;
                this.ChassiVeiculoTratado = !String.IsNullOrEmpty(carater.Veiculo.Chassi) ? carater.Veiculo.Chassi.Length > 4 ? carater.Veiculo.Chassi.Substring(carater.Veiculo.Chassi.Length - 5) : carater.Veiculo.Chassi : String.Empty;
                this.Renavam = carater.Veiculo.Renavam;
                this.Motor = carater.Veiculo.Motor;
                this.UltimoCRV = !String.IsNullOrEmpty(carater.Veiculo.UltimoCRV) ? Convert.ToDateTime(carater.Veiculo.UltimoCRV).ToShortDateString() : String.Empty;
                this.AtualizacaoVeiculo = carater.Veiculo.Atualizacao.ToString();
                this.Situacao = carater.Veiculo.Situacao;
                
                if (carater.Veiculo.Proprietario != null)
                    this.Proprietario = new ModeloDeProprietario(carater.Veiculo.Proprietario);

                if (carater.Veiculo.Possuidor != null)
                    this.Possuidor = new ModeloDePossuidor(carater.Veiculo.Possuidor);
            }

            if(situacoesBaixas.Contains(carater.SituacaoDoCarater))
            {
                this.RealizouBaixa = true;
                this.DescricaoLocalizacao = carater.DescricaoLocalizado;
                var dataHora = carater.DataHoraLocalizacao.HasValue ? carater.DataHoraLocalizacao.Value : DateTime.MinValue;
                this.CidadeLocalizacao = carater.CidadeLocalizado != null ? $" - Cidade: {carater.CidadeLocalizado.Descricao}" : String.Empty;
                this.UsuarioLocalizacao = $"Baixa por {usuario} no dia {dataHora.ToString("dd/MM")} às {dataHora.ToString("HH:mm")}";
            }

            carater.HistoricosDeCaraters.Where(a => !a.Excluido).OrderByDescending(a => a.DataDoCadastro).ToList().ForEach(a => this.HistoricosDeCaraters.Add(new ModeloDeHistoricoDeCaraterDaLista(a, carater.Fotos.ToList())));
            carater.HistoricosDePassagens.Where(a => !a.Excluido).OrderByDescending(a => a.DataDoCadastro).ToList().ForEach(a => this.HistoricosDePassagens.Add(new ModeloDeHistoricoDePassagensDaLista(a, ehCelular)));
            carater.Fotos.Where(a => a.Ativo).ToList().ForEach(a => this.Fotos.Add(new ModeloDeFotosDaLista(a)));
            
            this.SeloAtenas = carater.SeloAtenas;
            this.Excluido = carater.Excluido;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string ComplementoEndereco { get; set; }
        public string ComplementoEnderecoTradado { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public IEnumerable<SelectListItem> CidadesLocalizacao { get; set; }
        public int Crime { get; set; }
        public IEnumerable<SelectListItem> Crimes { get; set; }
        public int Veiculo { get; set; }
        public string UrlImagem { get; set; }
        public int SituacaoDoCarater { get; set; }
        public IEnumerable<SelectListItem> SituacoesDoCarater { get; set; }
        public int IdVeiculo { get; set; }
        public string Placa { get; set; }
        public string ModeloVeiculo { get; set; }
        public string MarcaVeiculo { get; set; }
        public string UfVeiculo { get; set; }
        public string MunicipioVeiculo { get; set; }
        public string AnoVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string ChassiVeiculo { get; set; }
        public string ChassiVeiculoTratado { get; set; }
        public string Renavam { get; set; }
        public string Motor { get; set; }
        public string UltimoCRV { get; set; }
        public string AtualizacaoVeiculo { get; set; }
        public string Situacao { get; set; }


        public string UsuarioCadastro { get; set; }
        public string CpfUsuario { get; set; }
        public bool RealizouBaixa { get; set; }
        public string DescricaoLocalizacao { get; set; }
        public string CidadeLocalizacao { get; set; }
        public string DataHoraLocalizacao { get; set; }
        public string UsuarioLocalizacao { get; set; }
        public string CssTipoCrime { get; set; }
        public string NomeCrime { get; set; }
        public string NomeCidade { get; set; }
        public string DescricaoHistorico { get; set; }
        public string DataHistorico { get; set; }
        public string HoraHistorico { get; set; }
        public bool SeloAtenas { get; set; }
        public int IdHistorico { get; set; }
        public bool Excluido { get; set; }
        public ModeloDeProprietario Proprietario { get; set; }
        public ModeloDePossuidor Possuidor { get; set; }

        public IList<ModeloDeHistoricoDeCaraterDaLista> HistoricosDeCaraters{ get; set; }
        public IList<ModeloDeFotosDaLista> Fotos { get; set; }
        public IList<ModeloDeHistoricoDePassagensDaLista> HistoricosDePassagens { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

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
