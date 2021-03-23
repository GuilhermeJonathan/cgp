using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeEstadio.Modelos
{
    public class ModeloDeFiltroDeEstadio
    {
        public ModeloDeFiltroDeEstadio()
        {
            this.Times = new List<SelectListItem>();
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Time { get; set; }
        public IEnumerable<SelectListItem> Times { get; set; }
    }
}
