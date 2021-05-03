using Cgp.Aplicacao.GestaoDeUsuariosSGPOL.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios
{
    public interface IServicoDeGestaoDeUsuariosSGPOL
    {
        Task<ModeloDeUsuarioSGPOL> BuscarDadosUsuario(string username, string password);
    }
}
