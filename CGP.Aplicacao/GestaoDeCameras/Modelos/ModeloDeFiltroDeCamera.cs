using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeCameras.Modelos
{
    public class ModeloDeFiltroDeCamera
    {
        public ModeloDeFiltroDeCamera()
        {
            this.Ativo = true;
            this.Cidades = new List<SelectListItem>();
        }

        public string Nome { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public bool Ativo { get; set; }
    }
}
