using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeCadastroDeRodada
    {
        public ModeloDeCadastroDeRodada()
        {
            this.Temporadas = new List<SelectListItem>();
        }

        public string Nome { get; set; }
        public int Temporada { get; set; }
        public IEnumerable<SelectListItem> Temporadas { get; set; }
    }
}
