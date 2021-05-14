using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cgp.Aplicacao.GestaoDeCaraters
{
    public interface IServicoDeGestaoDeCaraters
    {
        ModeloDeListaDeCaraters RetonarCaratersPorFiltro(ModeloDeFiltroDeCarater filtro, int pagina, int registrosPorPagina = 30);
        ModeloDeListaDeCaraters RetonarCaratersPorCidades(ModeloDeFiltroDeCarater filtro);
        ModeloDeEdicaoDeCarater BuscarCaraterPorId(int id, UsuarioLogado usuario);
        Tuple<string, int> CadastrarCarater(ModeloDeCadastroDeCarater modelo, UsuarioLogado usuario);
        string AlterarDadosDoCarater(ModeloDeEdicaoDeCarater modelo, UsuarioLogado usuario);
        string RealizarBaixaVeiculo(int id, string descricao, int cidade, UsuarioLogado usuario);
        ModeloDeListaDeCaraters BuscarCaraterPorPlaca(string placa);
        ModeloDeListaDeCaraters GerarPDFeRetornar(ModeloDeFiltroDeCarater filtro, UsuarioLogado usuario);
        bool VerificaCadastroDeCarater(string placa);
        Task<string> AdicionarFotos(int id, HttpFileCollectionBase files, UsuarioLogado usuario);
        string ExcluirFoto(int id, UsuarioLogado usuario);
        string AdicionarHistoricoPassagem(ModeloDeEdicaoDeCarater modelo, UsuarioLogado usuario);
        ModeloDeHistoricoDePassagensDaLista BuscarHistoricoDePassagem(int id);
    }
}
