using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class AlertaUsuario : Entidade
    {
        public AlertaUsuario()
        {

        }

        public AlertaUsuario(Alerta alerta, Usuario usuario)
        {
            this.Alerta = alerta;
            this.Usuario = usuario;
            this.Visualizado = true;
        }

        public Alerta Alerta { get; set; }
        public Usuario Usuario { get; set; }
        public bool Visualizado { get; set; } = false;
    }
}
