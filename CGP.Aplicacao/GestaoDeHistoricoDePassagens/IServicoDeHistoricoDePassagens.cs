using Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeHistoricoDePassagens
{
    public interface IServicoDeHistoricoDePassagens
    {
        ModeloDeListaDeHistoricoDePassagens RetornarHistociosDePassagensPorFiltro(ModeloDeFiltroDeHistoricoDePassagem filtro, int pagina, int registrosPorPagina = 30);
    }
}
