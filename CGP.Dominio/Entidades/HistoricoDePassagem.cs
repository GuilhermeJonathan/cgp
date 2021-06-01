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

        public HistoricoDePassagem(DateTime dataHora, string local, string placa, string arquivo, string latitude, string longitude)
        {
            this.Data = dataHora;
            this.Local = local;
            this.Placa = placa;
            this.Arquivo = arquivo;
            this.Latitude = latitude;
            this.Longitude = longitude;
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
        public ICollection<Alerta> Alertas { get; set; }
        public bool Excluido { get; set; } = false;

        public void RealizarBaixaAlertas()
        {
            if(this.Alertas != null)
            {
                foreach (var alerta in Alertas)
                    alerta.SituacaoDoAlerta = SituacaoDoAlerta.Finalizado;
            }
        }

        public void Excluir()
        {
            this.Excluido = true;
            this.RealizarBaixaAlertas();
        }
    }
}
