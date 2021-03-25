using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCrimes.Modelos
{
    public class ModeloDeFiltroDeCrime
    {
        public ModeloDeFiltroDeCrime()
        {
            this.Ativo = true;
        }

        public string Artigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
