using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Camera : Entidade
    {
        public Camera()
        {

        }

        public string Ponto { get; set; }
        public Cidade Cidade { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Endereco { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
