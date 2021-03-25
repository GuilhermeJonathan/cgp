using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Crime : Entidade
    {
        public Crime()
        {

        }

        public string Artigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Carater> Caraters { get; set; }
    }
}
