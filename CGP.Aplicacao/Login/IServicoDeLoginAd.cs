using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.Login
{
    public interface IServicoDeLoginAd
    {
        Usuario Autenticar(string userName, string password);
    }
}
