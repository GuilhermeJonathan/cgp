using Cgp.Aplicacao.GestaoDeCameras.Mdelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras
{
    public interface IServicoDeGestaDeCameras
    {
        ModeloDeCameraDaLista BuscarHistoricoDePassagem(string endereco);
    }
}
