using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios
{
    public interface IServicoDeGestaoDeUsuarios
    {
        string CadastrarNovoUsuario(ModeloDeCadastroDeUsuario modelo);
        string AlterarDadosDoUsuario(ModeloDeEdicaoDeUsuario modelo, UsuarioLogado usuario);
        string EditarMeusDados(ModeloDeEdicaoDeUsuario modelo, UsuarioLogado usuario);
        string AlterarSenha(ModeloDeEdicaoDeUsuario modelo);
        IList<ModeloDeUsuarioDaLista> RetonarTodosOsUsuariosAtivos();
        ModeloDeListaDeUsuarios RetonarUsuariosPorFiltro(ModeloDeFiltroDeUsuario filtro, int pagina, int registrosPorPagina = 30);
        string AtivarUsuario(int id, UsuarioLogado usuario);
        int BuscarUsuariosNovos();
        Usuario BuscarSomenteUsuarioPorId(int id);
        ModeloDeEdicaoDeUsuario BuscarUsuarioPorId(int id);
        ModeloDeEdicaoDeUsuario BuscarMeusDados(UsuarioLogado usuarioLogado);
        ModeloDeEdicaoDeUsuario BuscarUsuarioComHistoricoPorId(int id);
        ModeloDeListaDeHistoricosFinanceiros BuscarHistoricosParaSaque(ModeloDeFiltroDeHistoricoFinanceiro filtro);
    }
}
