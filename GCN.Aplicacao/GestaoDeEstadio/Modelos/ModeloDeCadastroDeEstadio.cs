using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeEstadio.Modelos
{
    public class ModeloDeCadastroDeEstadio
    {
        public ModeloDeCadastroDeEstadio()
        {
            this.Times = new List<SelectListItem>();
        }

        public string Nome { get; set; }
        public string Cidade { get; set; }
        public int Time { get; set; }
        public IEnumerable<SelectListItem> Times { get; set; }
    }
}
