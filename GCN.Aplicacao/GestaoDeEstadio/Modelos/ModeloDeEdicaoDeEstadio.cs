using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDeEstadio.Modelos
{
    public class ModeloDeEdicaoDeEstadio
    {
        public ModeloDeEdicaoDeEstadio()
        {
            this.Times = new List<SelectListItem>();
        }

        public ModeloDeEdicaoDeEstadio(Estadio estadio)
        {
            this.Times = new List<SelectListItem>();
            this.Id = estadio.Id;
            this.Nome = estadio.Nome;
            this.Cidade = estadio.Cidade;
            this.Ativo = estadio.Ativo;
            this.Time = estadio.Time != null ? estadio.Time.Id : 0 ;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }
        public int Time { get; set; }
        public IEnumerable<SelectListItem> Times { get; set; }
    }
}
