using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos
{
    public class ModeloDeHistoricoDePassagensDaLista
    {
        public ModeloDeHistoricoDePassagensDaLista(HistoricoDePassagem historico, bool ehCelular, Camera camera = null)
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
            } else if(historico.TipoDeHistoricoDePassagem == TipoDeHistoricoDePassagem.Manual)
            {
                if (!String.IsNullOrEmpty(historico.Arquivo))
                {
                    var caminhoBlob = $"{VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob")}fotos/{historico.Arquivo}";
                    this.Arquivo = caminhoBlob;
                }
            }
            else if (!String.IsNullOrEmpty(historico.Arquivo))
            {
                this.Arquivo = caminho;
            }

            if(camera != null)
            {
                this.TemLatLong = true;
                this.Cidade = camera.Cidade.Descricao;
                this.Latitude = camera.Latitude;
                this.Longitude = camera.Longitude;
                this.LocalParaMapa = $"<b>{camera.Cidade.Descricao}</b><br/>{historico.Local}";
            } else
            {
                this.TemLatLong = !String.IsNullOrEmpty(historico.Latitude) && !String.IsNullOrEmpty(historico.Longitude) ? true : false;
                this.Latitude = historico.Latitude;
                this.Longitude = historico.Longitude;
                this.LocalParaMapa = $"{historico.Local}";
            }

            this.IdCarater = historico.Carater != null ? historico.Carater.Id : 0;
        }

        public ModeloDeHistoricoDePassagensDaLista(HistoricoDePassagem historico, List<Camera> cameras)
        {
            if (historico == null)
                return;

            if(cameras != null)
            {
                var camera = cameras.FirstOrDefault(a => a.Nome == historico.Local);
                if (camera != null)
                {
                    this.TemLatLong = true;
                    this.Cidade = camera.Cidade.Descricao;
                    this.Latitude = camera.Latitude;
                    this.Longitude = camera.Longitude;
                    this.LocalParaMapa = $"<b>{camera.Cidade.Descricao}</b><br/>{historico.Local}";
                }
                else
                {
                    this.TemLatLong = !String.IsNullOrEmpty(historico.Latitude) && !String.IsNullOrEmpty(historico.Longitude) ? true : false;
                    this.Latitude = historico.Latitude;
                    this.Longitude = historico.Longitude;
                    this.Cidade = historico.Carater.Cidade != null ? historico.Carater.Cidade.Descricao : String.Empty;
                }
            }

            this.Id = historico.Id;
            this.DataPassagem = historico.Data.ToString("dd/MM/yyyy HH:mm");
            this.Local = historico.Local;
            this.TipoHistorico = historico.TipoDeHistoricoDePassagem.ToString();
            if (historico.Carater != null)
            {
                this.IdCarater = historico.Carater != null ? historico.Carater.Id : 0;
                this.Placa = historico.Carater.Veiculo != null ? historico.Carater.Veiculo.Placa : String.Empty;
            }
        }

        public int Id { get; set; }
        public string DataPassagem { get; set; }
        public string Local { get; set; }
        public string LocalParaMapa { get; set; }
        public string Arquivo { get; set; }
        public string Cidade { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int IdCarater { get; set; }
        public string Placa { get; set; }
        public string TipoHistorico { get; set; }
        public bool ExisteArquivo { get; set; }
        public bool TemLatLong { get; set; }
    }
}
