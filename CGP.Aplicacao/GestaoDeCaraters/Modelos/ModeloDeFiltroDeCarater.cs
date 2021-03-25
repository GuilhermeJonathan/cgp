using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeFiltroDeCarater
    {

        public ModeloDeFiltroDeCarater()
        {
            this.Crimes = new List<SelectListItem>();
            this.Cidades = new List<SelectListItem>();
        }

        public string DataHora { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public int Crime { get; set; }
        public IEnumerable<SelectListItem> Crimes { get; set; }
    }
}
