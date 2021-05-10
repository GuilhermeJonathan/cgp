using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Carater : Entidade
    {
        public Carater()
        {
            this.HistoricosDeCaraters = new List<HistoricoDeCarater>();
            this.HistoricoDePassagem = new List<HistoricoDePassagem>();
            this.Fotos = new List<Foto>();
        }

        public Carater(string descricao, string complementoEndereco, DateTime dataHora, Veiculo veiculo, Cidade cidade, Crime crime, string urlImagem, Usuario usuario)
        {
            this.Descricao = descricao;
            this.ComplementoEndereco = complementoEndereco;
            this.DataHoraDoFato = dataHora;
            this.Veiculo = veiculo;
            this.Cidade = cidade;
            this.Crime = crime;
            this.UrlImagem = urlImagem;
            this.UsuarioQueAlterou = usuario;
            this.SituacaoDoCarater = SituacaoDoCarater.Cadastrado;
        }

        public string Descricao { get; set; }
        public string ComplementoEndereco { get; set; }
        public DateTime? DataHoraDoFato { get; set; }
        public Veiculo Veiculo { get; set; }
        public Cidade Cidade { get; set; }
        public Crime Crime { get; set; }
        public SituacaoDoCarater SituacaoDoCarater { get; set; }
        public string UrlImagem { get; set; }
        public Usuario UsuarioQueAlterou { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string DescricaoLocalizado { get; set; }
        public Cidade CidadeLocalizado { get; set; }
        public DateTime? DataHoraLocalizacao { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public ICollection<HistoricoDeCarater> HistoricosDeCaraters { get; set; }
        public ICollection<HistoricoDePassagem> HistoricoDePassagem { get; set; }
        public bool VerificarSeJaTemEssaFoto(string descricao) => this.Fotos.Any(a => a.Caminho == descricao && a.Ativo);

        public void AlterarDados(string descricao, string complementoEndereco, DateTime dataHora, Cidade cidade, Crime crime, Veiculo veiculo, string urlImagem, Usuario usuario)
        {
            var descricaoHistorico = String.Empty;

            this.Descricao = descricao;
            this.ComplementoEndereco = complementoEndereco;
            this.DataHoraDoFato = dataHora;
            this.Cidade = cidade;
            this.Crime = crime;
            this.Veiculo = veiculo;
            this.UrlImagem = urlImagem;
            this.Atualizar(usuario);

            this.AdicionarHistorico(new HistoricoDeCarater("Alterou os dados do Caráter", descricaoHistorico, TipoDeHistoricoDeCarater.Historico, usuario, this.Id));
        }

        public void Atualizar(Usuario usuario)
        {
            this.DataUltimaAtualizacao = DateTime.Now;
            //this.UsuarioQueAlterou = usuario;
        }

        public void RealizarBaixaVeiculo(string descricao, Cidade cidadeLocalizado, Usuario usuario)
        {
            this.DescricaoLocalizado = descricao;
            this.CidadeLocalizado = cidadeLocalizado;
            this.SituacaoDoCarater = SituacaoDoCarater.Localizado;
            this.DataHoraLocalizacao = DateTime.Now;
            this.UsuarioQueAlterou = usuario;
            this.Atualizar(usuario);
        }

        public void RealizarBaixaAutomatica(string descricao, Usuario usuario)
        {
            this.DescricaoLocalizado = descricao;
            this.SituacaoDoCarater = SituacaoDoCarater.BaixaAutomatica;
            this.DataHoraLocalizacao = DateTime.Now;
            this.UsuarioQueAlterou = usuario;
            this.Atualizar(usuario);
        }

        public void AdicionarHistorico(HistoricoDeCarater historico)
        {
            if (this.HistoricosDeCaraters == null)
                this.HistoricosDeCaraters = new List<HistoricoDeCarater>();

            this.DataUltimaAtualizacao = DateTime.Now;
            this.HistoricosDeCaraters.Add(historico);
        }
    }
}
