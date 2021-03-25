using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeCadastroDeCarater
    {

        public ModeloDeCadastroDeCarater()
        {
            this.Crimes = new List<SelectListItem>();
            this.Cidades = new List<SelectListItem>();
            this.Data = DateTime.Now.ToShortDateString();
        }

        public string Descricao { get; set; }
        public string ComplementoEndereco { get; set; }
        public string UrlImagem { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public int Cidade { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public int Crime { get; set; }
        public IEnumerable<SelectListItem> Crimes { get; set; }
        public int Veiculo { get; set; }
        public string Placa { get; set; }
        public string ModeloVeiculo { get; set; }
        public string MarcaVeiculo { get; set; }
        public string AnoVeiculo{ get; set; }
        public string CorVeiculo { get; set; }
    }
}
