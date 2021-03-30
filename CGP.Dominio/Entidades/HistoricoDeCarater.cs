using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class HistoricoDeCarater : Entidade
    {
        public HistoricoDeCarater()
        {

        }

        public string Descricao { get; set; }
        public Usuario Usuario { get; set; }
        public Carater Carater { get; set; }
    }
}
