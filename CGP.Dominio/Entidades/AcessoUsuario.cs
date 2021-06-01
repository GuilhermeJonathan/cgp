using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class AcessoUsuario : Entidade
    {
        public AcessoUsuario(Usuario usuario)
        {
            this.Usuario = usuario;
        }

        public Usuario Usuario { get; set; }
    }
}
