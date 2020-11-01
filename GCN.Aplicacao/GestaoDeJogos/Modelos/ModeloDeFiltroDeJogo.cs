using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeJogos.Modelos
{
    public class ModeloDeFiltroDeJogo
    {
        public ModeloDeFiltroDeJogo()
        {
            this.Times = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
        }

        public string DataHoraDoJogo{ get; set; }
        public int Time { get; set; }
        public IEnumerable<SelectListItem> Times { get; set; }
        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
 
    }
}
