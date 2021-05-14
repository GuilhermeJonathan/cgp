using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class HistoricoDePassagem : Entidade
    {
        public HistoricoDePassagem()
        {

        }

        public HistoricoDePassagem(DateTime dataHora, string local, string placa, string arquivo)
        {
            this.Data = dataHora;
            this.Local = local;
            this.Placa = placa;
            this.Arquivo = arquivo;
            this.TipoDeHistoricoDePassagem = TipoDeHistoricoDePassagem.Manual;
        }

        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public string Placa { get; set; }
        public string Arquivo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Carater Carater { get; set; }
        public TipoDeHistoricoDePassagem TipoDeHistoricoDePassagem { get; set; }
    }
}
