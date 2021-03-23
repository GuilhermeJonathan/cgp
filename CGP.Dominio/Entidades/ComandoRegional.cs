using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class ComandoRegional : Entidade
    {
        public ComandoRegional()
        {
            this.Batalhoes = new List<Batalhao>();
        }

        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Batalhao> Batalhoes { get; set; }
    }
}
