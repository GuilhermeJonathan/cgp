using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeCameras.Modelos
{
    public class ModeloDeEdicaoDeCamera
    {
        public ModeloDeEdicaoDeCamera()
        {
            this.Cidades = new List<SelectListItem>();
        }

        public ModeloDeEdicaoDeCamera(Camera camera)
        {
            if (camera == null)
                return;

            this.Id = camera.Id;
            this.Nome = camera.Nome;
            this.Ponto = camera.Ponto;
            this.Latitude = camera.Latitude;
            this.Longitude = camera.Longitude;
            this.Cidade = camera.Cidade.Id;
            this.Ativo = camera.Ativo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ponto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public bool Ativo { get; set; }
    }
}
