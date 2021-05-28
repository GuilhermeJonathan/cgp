using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras.Mdelos
{
    public class ModeloDeCameraDaLista
    {
        public ModeloDeCameraDaLista()
        {

        }

        public ModeloDeCameraDaLista(Camera camera)
        {
            this.Cidade = camera.Cidade.Descricao;
            this.Endereco = camera.Endereco;
            this.Lat = camera.Latitude;
            this.Long = camera.Longitude;
        }

        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}
