using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeRodada.Modelos
{
    public class ModeloDeFiltroDeRodada
    {
        public ModeloDeFiltroDeRodada()
        {
            this.Ativo = true;       
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Temporada { get; set; }

    }
}
