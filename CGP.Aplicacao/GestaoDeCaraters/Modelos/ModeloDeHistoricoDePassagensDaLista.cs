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
        public ModeloDeHistoricoDePassagensDaLista(HistoricoDePassagem historico)
        {
            if (historico == null)
                return;

            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\FotosPlacas";
            this.Id = historico.Id;
            this.DataPassagem = historico.Data.ToString("dd/MM/yyyy HH:mm");
            this.Local = historico.Local;
            
            var arquivoTratado = historico.Arquivo.Replace("Images/", "");
            var caminho = VariaveisDeAmbiente.Pegar<string>("LOCAL:servidorDePassagens") + @"\\images\\" + arquivoTratado;
            this.ExisteArquivo = File.Exists(VariaveisDeAmbiente.Pegar<string>("LOCAL:servidorDePassagens") + @"//images//" + arquivoTratado);
            
            if (this.ExisteArquivo)
            {
                var arquivo = Path.GetFileName(caminho);
                var destino = Path.Combine(path, arquivo);
                File.Copy(caminho, destino, true);
                this.Arquivo = "../../FotosPlacas/" + arquivo;
            } 
            else if(historico.TipoDeHistoricoDePassagem == TipoDeHistoricoDePassagem.Automatico)
            {
                var arquivo = Path.GetFileName(caminho);
                this.Arquivo = "../../FotosPlacas/" + arquivo;
            }
            else if (!String.IsNullOrEmpty(historico.Arquivo))
            {
                this.Arquivo = historico.Arquivo;
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
    }
}
