using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeFiltroDeRodada
    {
        public ModeloDeFiltroDeRodada()
        {
            this.Rodadas = new List<SelectListItem>();
            this.Ativo = true;       
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Temporada { get; set; }
        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }

    }
}
