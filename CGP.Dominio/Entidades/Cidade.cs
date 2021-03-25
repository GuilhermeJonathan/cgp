using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Cidade
    {
        public int Id { get; set; }
        public int CodigoCidade { get; set; }
        public Uf Uf { get; set; }
        public string Descricao { get; set; }
        public string Cep { get; set; }
        public string Sigla { get; set; }
    }
}
