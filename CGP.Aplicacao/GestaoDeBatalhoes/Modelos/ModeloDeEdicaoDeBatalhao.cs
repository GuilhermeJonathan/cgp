using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeBatalhoes.Modelos
{
    public class ModeloDeEdicaoDeBatalhao
    {
        public ModeloDeEdicaoDeBatalhao()
        {
            this.ComandosRegionais = new List<SelectListItem>(); ;
        }

        public ModeloDeEdicaoDeBatalhao(Batalhao batalhao)
        {
            this.Id = batalhao.Id;
            this.Nome = batalhao.Nome;
            this.Sigla = batalhao.Sigla;
            this.Cidade= batalhao.Cidade;
            this.Ativo = batalhao.Ativo;
            this.ComandoRegional = batalhao.ComandoRegional != null ? batalhao.ComandoRegional.Id : 0;
            this.DataDoCadastro = batalhao.DataDoCadastro.ToShortDateString();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }
        public string DataDoCadastro { get; set; }
        public int ComandoRegional { get; set; }
        public IEnumerable<SelectListItem> ComandosRegionais { get; set; }
    }
}
