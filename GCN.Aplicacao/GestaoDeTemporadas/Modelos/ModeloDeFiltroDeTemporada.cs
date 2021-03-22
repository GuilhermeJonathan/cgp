using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTemporadas.Modelos
{
    public class ModeloDeFiltroDeTemporada
    {
        public ModeloDeFiltroDeTemporada()
        {
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Ano { get; set; }
        public string Pais { get; set; }
        public bool Ativo { get; set; }
    }
}
