using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios
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
        string CadastrarSaldo(int id, decimal saldo, UsuarioLogado usuario, string tipoDeSolicitacaoFinanceira);
        string CadastrarSaldoParaPremiacao(Usuario usuario, decimal saldo, UsuarioLogado usuarioLogado, string textoPremiacao);
        int BuscarUsuariosNovos();
        ModeloDeEdicaoDeUsuario BuscarUsuarioPorId(int id);
        ModeloDeEdicaoDeUsuario BuscarMeusDados(UsuarioLogado usuarioLogado);
        ModeloDeEdicaoDeUsuario BuscarUsuarioComHistoricoPorId(int id);
        string RetirarSaldo(decimal saldo, UsuarioLogado usuario, int tipoDePix, string chavePix);
    }
}
