using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Alerta : Entidade
    {
        public Alerta()
        {

        }

        public HistoricoDePassagem HistoricoDePassagem { get; set; }
        public Usuario Usuario { get; set; }
        public SituacaoDoAlerta SituacaoDoAlerta { get; set; } = SituacaoDoAlerta.Cadastrado;
    }
}
