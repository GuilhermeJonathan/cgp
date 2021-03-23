using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDePremiacoes.Modelos
{
    public class ModeloDeFiltroDePremiacao
    {
        public ModeloDeFiltroDePremiacao()
        {
            this.Usuarios = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
        }

        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
        public int Usuario { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
    }
}
