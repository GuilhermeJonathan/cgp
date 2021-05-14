using Cgp.Aplicacao.Util;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeFiltroDeUsuario
    {
        public ModeloDeFiltroDeUsuario()
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
            this.Batalhoes = new List<SelectListItem>();
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public int Batalhao { get; set; }
        public IEnumerable<SelectListItem> Batalhoes { get; set; }
        public int PerfilDeUsuario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeUsuario { get; set; }
        public bool Ativo { get; set; }
    }
}
