using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeAlertaDaLista
    {
        public ModeloDeAlertaDaLista()
        {

        }

        public ModeloDeAlertaDaLista(Alerta alerta)
        {
            if (alerta == null)
                return;

            this.Id = alerta.Id;
            var arquivoTratado = alerta.HistoricoDePassagem.Arquivo.Replace(@"I:\", "").Replace(@"\", "/");
            var caminho = VariaveisDeAmbiente.Pegar<string>("LOCAL:servidorDePassagens") + arquivoTratado;
            this.DataPassagem = alerta.HistoricoDePassagem.Data.ToString("dd/MM/yyyy HH:mm");
            this.Local = alerta.HistoricoDePassagem.Local;
            this.Arquivo = caminho;
            this.IdCarater = alerta.HistoricoDePassagem != null ? alerta.HistoricoDePassagem.Carater.Id : 0;
            this.Placa = alerta.HistoricoDePassagem != null ? alerta.HistoricoDePassagem.Carater.Veiculo.Placa : String.Empty;
        }

        public int Id { get; set; }
        public int IdCarater { get; set; }
        public string Placa{ get; set; }
        public string DataPassagem { get; set; }
        public string Local { get; set; }
        public string Arquivo { get; set; }

    }
}
