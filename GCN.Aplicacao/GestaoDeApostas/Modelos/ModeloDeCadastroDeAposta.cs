using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeApostas.Modelos
{
    public class ModeloDeCadastroDeAposta
    {
        public ModeloDeCadastroDeAposta()
        {
            this.Usuarios = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
        }

        public int Usuario { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
    }
}
