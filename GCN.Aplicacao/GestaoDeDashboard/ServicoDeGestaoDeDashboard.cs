using Cgp.Aplicacao.GestaoDeDashboard.Modelos;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeDashboard
{
    public class ServicoDeGestaoDeDashboard : IServicoDeGestaoDeDashboard
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public ServicoDeGestaoDeDashboard(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        public ModeloDeListaDeDashboard RetonarDashboardPorFiltro(ModeloDeFiltroDeDashboard filtro, UsuarioLogado usuario)
        {
            var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
            filtro.Usuario = usuario.PerfilDeUsuario != PerfilDeUsuario.Administrador ? usuarioBanco.Id : 0;

            var premiacoes = this._servicoExternoDePersistencia.RepositorioDePremiacoes.RetornarPremiacoesPorTemporada(filtro.Usuario);
            var modelo = new ModeloDeListaDeDashboard(premiacoes, 0, filtro); ;

            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.RetornarTodosUsuarios();

            if(usuarios != null)
            {
                var saques = this._servicoExternoDePersistencia.RepositorioDeUsuarios.RetornarHistoricosFinanceirosDeSaques();
                var valorCaixa = usuarios.Sum(a => a.Saldo).ToString("f");
                modelo.ValorCaixa = valorCaixa;
                modelo.TotalSaques = saques.Count.ToString();
            }

            return modelo;
        }

    }
}
