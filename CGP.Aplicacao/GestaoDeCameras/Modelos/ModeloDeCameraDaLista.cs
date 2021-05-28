using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras.Modelos
{
    public class ModeloDeCameraDaLista
    {
        public ModeloDeCameraDaLista()
        {

        }

        public ModeloDeCameraDaLista(Camera camera)
        {
            if (camera == null)
                return;

            this.Id = camera.Id;
            this.Cidade = camera.Cidade.Descricao;
            this.Nome = camera.Nome;
            this.Ponto = camera.Ponto;
            this.Latitude = camera.Latitude;
            this.Longintude = camera.Longitude;
            this.Ativo = camera.Ativo;
        }

        public int Id { get; set; }
        public string Cidade { get; set; }
        public string Nome { get; set; }
        public string Ponto { get; set; }
        public string Latitude { get; set; }
        public string Longintude { get; set; }
        public bool Ativo { get; set; }
    }
}
