using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeHistoricoDeCaraterDaLista
    {
        public ModeloDeHistoricoDeCaraterDaLista()
        {

        }

        public ModeloDeHistoricoDeCaraterDaLista(HistoricoDeCarater historico)
        {
            if (historico == null)
                return;

            this.Id = historico.Id;
            this.Descricao = historico.Descricao;
            this.IdUsuario = historico.Usuario != null ? historico.Usuario.Id : 0;
            this.IdCarater = historico.Carater != null ? historico.Carater.Id : 0;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdUsuario { get; set; }
        public int IdCarater { get; set; }

    }
}
