using Cgp.Aplicacao.Util;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
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

        public ModeloDeHistoricoDeCaraterDaLista(HistoricoDeCarater historico, List<Foto> fotos)
        {
            var exibeBotao = new List<TipoDeHistoricoDeCarater> { TipoDeHistoricoDeCarater.HistoricoCarater };
            var exibeDescricao = new List<TipoDeHistoricoDeCarater> { TipoDeHistoricoDeCarater.Historico, TipoDeHistoricoDeCarater.Baixa };

            if (historico == null)
                return;

            this.Id = historico.Id;
            this.DataDoCadastro = historico.DataDoCadastro.ToString("dd.MMMM.yyyy");
            this.Descricao = !String.IsNullOrEmpty(historico.Descricao) ? historico.Descricao.Replace(Environment.NewLine, "<br />").Replace("\n", "<br />") : "";
            this.Titulo = historico.Titulo;
            this.IdUsuario = historico.Usuario != null ? historico.Usuario.Id : 0;
            
            this.Usuario = historico.Usuario != null ? historico.Usuario.PerfilDeUsuario == PerfilDeUsuario.Atenas 
                ? historico.Usuario.PerfilDeUsuario.ToString() : historico.Usuario.Nome.Valor : "";
            this.IdCarater = historico.Carater != null ? historico.Carater.Id : 0;
            this.IdEntidade = historico.IdEntidade;
            this.TipoDeHistoricoDeCarater = historico.TipoDeHistoricoDeCarater;

            this.IconeDoTipo = RetornaIconeDoTipo(historico.TipoDeHistoricoDeCarater);
            this.TempoDecorrido = TimeAgo(historico.DataDoCadastro);

            this.ExibeBotao = exibeBotao.Contains(historico.TipoDeHistoricoDeCarater);
            
            this.ExibeDescricao = exibeDescricao.Contains(historico.TipoDeHistoricoDeCarater);

            if (fotos != null && fotos.Count > 0)
            {
                var caminhoBlob = VariaveisDeAmbiente.Pegar<string>("azure:caminhoDoBlob");
                var foto = fotos.FirstOrDefault(a => a.Id == historico.IdEntidade);
                this.UrlFoto = foto != null ? $"{caminhoBlob}fotos/{foto.Caminho}" : String.Empty;
            }
        }

        public int Id { get; set; }
        public int IdCarater { get; set; }
        public int IdEntidade { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string TempoDecorrido { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string DataDoCadastro { get; set; }
        public string IconeDoTipo { get; set; }
        public bool ExibeBotao { get; set; }
        public bool ExibeDescricao { get; set; }
        public string UrlFoto { get; set; }

        public TipoDeHistoricoDeCarater TipoDeHistoricoDeCarater { get; set; }

        private string RetornaIconeDoTipo(TipoDeHistoricoDeCarater tipoDeHistoricoDeCarater)
        {
            var retorno = String.Empty;

            switch (tipoDeHistoricoDeCarater)
            {
                case TipoDeHistoricoDeCarater.Criacao:
                    retorno = "fas fa-cog bg-purple";
                    break;
                case TipoDeHistoricoDeCarater.Historico:
                    retorno = "fas fa-comments bg-blue";
                    break;
                case TipoDeHistoricoDeCarater.Passagem:
                    retorno = "fas fa-user bg-aqua";
                    break;
                case TipoDeHistoricoDeCarater.Foto:
                    retorno = "fas fa-camera bg-green";
                    break;
                case TipoDeHistoricoDeCarater.Baixa:
                    retorno = "fas fa-arrow-alt-circle-down bg-yellow";
                    break;
                case TipoDeHistoricoDeCarater.HistoricoPassagem:
                    retorno = "fas fa-car-side bg-blue";
                    break;
                case TipoDeHistoricoDeCarater.HistoricoCarater:
                    retorno = "fas fa-comments bg-green";
                    break;
                case TipoDeHistoricoDeCarater.Exclusao:
                    retorno = "fas fa-times bg-red";
                    break;
                default:
                    break;
            }
            return retorno;
        }

        private string TimeAgo(DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} segundos", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("há {0} minutos", timeSpan.Minutes) :
                    "há um minuto";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("há {0} horas", timeSpan.Hours) :
                    "há uma hora";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("há {0} dias", timeSpan.Days) :
                    "ontem";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 60 ?
                    String.Format("há {0} meses", timeSpan.Days / 30) :
                    "há um mês";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("há {0} anos", timeSpan.Days / 365) :
                    "há um ano";
            }

            return result;
        }
    }
}
