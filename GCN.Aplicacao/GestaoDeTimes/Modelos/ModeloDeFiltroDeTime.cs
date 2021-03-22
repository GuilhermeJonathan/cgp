using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeTimes.Modelos
{
    public class ModeloDeFiltroDeTime
    {
        public ModeloDeFiltroDeTime()
        {
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
