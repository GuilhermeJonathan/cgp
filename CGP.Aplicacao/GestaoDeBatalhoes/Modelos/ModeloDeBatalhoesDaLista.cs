using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeBatalhoes.Modelos
{
    public class ModeloDeBatalhoesDaLista : Modelo<Batalhao>
    {
        public ModeloDeBatalhoesDaLista()
        {

        }

        public ModeloDeBatalhoesDaLista(Batalhao batalhao)
        {
            var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");
            this.Id = batalhao.Id;
            this.Nome = batalhao.Nome;
            this.Sigla = batalhao.Sigla;
            this.Cidade = batalhao.Cidade != null ? batalhao.Cidade.Descricao : String.Empty;
            this.NomeComando = batalhao.ComandoRegional != null ? batalhao.ComandoRegional.Sigla : String.Empty;
            this.DataDoCadastro = batalhao.DataDoCadastro.ToShortDateString();
            this.Ativo = batalhao.Ativo ? "Sim" : "Não";
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Cidade { get; set; }
        public string NomeComando { get; set; }
        public string Ativo { get; set; }
        public string DataDoCadastro { get; set; }
    }
}
