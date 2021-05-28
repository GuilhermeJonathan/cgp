using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeCameras.Modelos
{
    public class ModeloDeCadastroDeCamera
    {
        public ModeloDeCadastroDeCamera()
        {
            this.Ativo = true;
            this.Cidades = new List<SelectListItem>();
        }

        public string Nome { get; set; }
        public string Ponto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public bool Ativo { get; set; }
    }
}
