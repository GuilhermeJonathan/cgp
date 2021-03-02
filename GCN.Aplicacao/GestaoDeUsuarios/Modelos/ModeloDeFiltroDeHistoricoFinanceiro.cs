using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeFiltroDeHistoricoFinanceiro
    {
        public ModeloDeFiltroDeHistoricoFinanceiro()
        {
            this.Usuarios = new List<SelectListItem>();
        }

        public int Usuario { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public bool Ativo { get; set; }
    }
}
