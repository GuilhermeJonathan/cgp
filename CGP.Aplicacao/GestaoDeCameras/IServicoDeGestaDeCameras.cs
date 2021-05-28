using Cgp.Aplicacao.GestaoDeCameras.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras
{
    public interface IServicoDeGestaDeCameras
    {
        ModeloDeListaDeCameras RetonarCamerasPorFiltro(ModeloDeFiltroDeCamera filtro, int pagina, int registrosPorPagina = 30);
        Camera BuscarCameraPorEndereco(string endereco);
        string CadastrarCamera(ModeloDeCadastroDeCamera modelo, UsuarioLogado usuario);
        ModeloDeEdicaoDeCamera BuscarCaraterPorId(int id, UsuarioLogado usuario);
        string AlterarDadosDaCamera(ModeloDeEdicaoDeCamera modelo, UsuarioLogado usuario);
        string AtivarCamera(int id, UsuarioLogado usuario);
    }
}
