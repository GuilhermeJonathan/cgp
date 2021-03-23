using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeBatalhoes.Modelos
{
    public class ModeloDeFiltroDeBatalhao
    {
        public ModeloDeFiltroDeBatalhao()
        {
            this.Ativo = true;
            this.ComandosRegionais = new List<SelectListItem>();
            this.Cidades = new List<SelectListItem>();
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int ComandoRegional { get; set; }
        public IEnumerable<SelectListItem> ComandosRegionais { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
    }
}
