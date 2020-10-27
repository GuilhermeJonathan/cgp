using GCN.Aplicacao.Util;
using GCN.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GCN.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeCadastroDeUsuario
    {
        public ModeloDeCadastroDeUsuario()
        {
            this.PerfisDeUsuario = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<PerfilDeUsuario>();
        }

        public string Nome { get; set; }
        public PerfilDeUsuario PerfilDeUsuario { get; set; }
        public IEnumerable<SelectListItem> PerfisDeUsuario { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
