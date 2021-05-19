using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeHistoricoDePassagensDaLista
    {
        public ModeloDeHistoricoDePassagensDaLista(HistoricoDePassagem historico, bool ehCelular)
        {
            if (historico == null)
                return;

            this.Id = historico.Id;
            this.DataPassagem = historico.Data.ToString("dd/MM/yyyy HH:mm");
            this.Local = historico.Local;
            
            var arquivoTratado = historico.Arquivo.Replace(@"I:\", "").Replace(@"\", @"/");
            var caminho = VariaveisDeAmbiente.Pegar<string>("LOCAL:servidorDePassagens") + arquivoTratado;

            if (ehCelular) caminho.Replace("https://", "http://");
            if(historico.TipoDeHistoricoDePassagem == TipoDeHistoricoDePassagem.Automatico)
            {
                this.Arquivo = caminho;
            }
            else if (!String.IsNullOrEmpty(historico.Arquivo))
            {
                this.Arquivo = caminho;
            }

            this.Latitude = historico.Latitude;
            this.Longitude = historico.Longitude;
            this.IdCarater = historico.Carater != null ? historico.Carater.Id : 0;
        }

        public int Id { get; set; }
        public string DataPassagem { get; set; }
        public string Local { get; set; }
        public string Arquivo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int IdCarater { get; set; }
        public Stream Imagem { get; set; }
        public bool ExisteArquivo { get; set; }

        //string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\FotosPlacas";
        // this.ExisteArquivo = File.Exists(VariaveisDeAmbiente.Pegar<string>("LOCAL:servidorDePassagens") + arquivoTratado);

        //    if (this.ExisteArquivo)
        //    {
        //    var arquivo = Path.GetFileName(caminho);
        //    var destino = Path.Combine(path, arquivo);
        //    File.Copy(caminho, destino, true);
        //    this.Arquivo = "../../FotosPlacas/" + arquivo;
        //}
    }
}
