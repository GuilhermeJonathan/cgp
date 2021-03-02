using Campeonato.Aplicacao.GestaoDeDashboard.Modelos;
using Campeonato.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeDashboard
{
    public interface IServicoDeGestaoDeDashboard
    {
        ModeloDeListaDeDashboard RetonarDashboardPorFiltro(ModeloDeFiltroDeDashboard filtro, UsuarioLogado usuario);
    }
}
