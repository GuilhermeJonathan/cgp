using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais.Modelos
{
    public class ModeloDeFiltroDeComandoRegional
    {
        public ModeloDeFiltroDeComandoRegional()
        {
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
