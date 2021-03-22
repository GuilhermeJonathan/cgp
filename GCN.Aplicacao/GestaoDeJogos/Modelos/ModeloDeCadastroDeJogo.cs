using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeJogos.Modelos
{
    public class ModeloDeCadastroDeJogo
    {
        public ModeloDeCadastroDeJogo()
        {
            this.Times1 = new List<SelectListItem>();
            this.Times2 = new List<SelectListItem>();
            this.Estadios = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
            this.Temporadas = new List<SelectListItem>();
        }

        public string DataDoJogo { get; set; }
        public string HoraDoJogo { get; set; }
        public int Time1 { get; set; }
        public IEnumerable<SelectListItem> Times1 { get; set; }
        public int Time2 { get; set; }
        public IEnumerable<SelectListItem> Times2 { get; set; }
        public int Estadio { get; set; }
        public IEnumerable<SelectListItem> Estadios { get; set; }
        public int Temporada { get; set; }
        public IEnumerable<SelectListItem> Temporadas { get; set; }
        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
    }
}
