using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Carater : Entidade
    {
        public Carater()
        {

        }

        public string Descricao { get; set; }
        public string ComplementoEndereco { get; set; }
        public DateTime? DataHora { get; set; }
        public Veiculo Veiculo { get; set; }
        public Cidade Cidade { get; set; }
        public Crime Crime { get; set; }
        public SituacaoDoCarater SituacaoDoCarater { get; set; }
    }
}
