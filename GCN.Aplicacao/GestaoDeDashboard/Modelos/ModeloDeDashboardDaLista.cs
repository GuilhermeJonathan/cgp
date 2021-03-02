using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeDashboard.Modelos
{
    public class ModeloDeDashboardDaLista
    {
        public ModeloDeDashboardDaLista()
        {

        }

        public ModeloDeDashboardDaLista(Premiacao premiacao)
        {

        }

        public string Nome { get; set; }
        public string Valor { get; set; }

    }
}
