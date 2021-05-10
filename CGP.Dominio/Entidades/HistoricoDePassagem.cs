using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class HistoricoDePassagem : Entidade
    {
        public HistoricoDePassagem()
        {

        }

        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public string Placa { get; set; }
        public string Arquivo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Carater Carater { get; set; }
    }
}
