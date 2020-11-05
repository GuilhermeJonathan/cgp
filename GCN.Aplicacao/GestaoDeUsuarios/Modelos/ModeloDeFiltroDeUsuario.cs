using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeFiltroDeUsuario
    {
        public ModeloDeFiltroDeUsuario()
        {
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Email { get; set; }

        public bool Ativo { get; set; }
    }
}
