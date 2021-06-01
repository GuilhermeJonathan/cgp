using Cgp.Aplicacao.GestaoDeCaraters.Modelos;
using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos
{
    public class ModeloDeListaDeHistoricoDePassagens
    {
        public ModeloDeListaDeHistoricoDePassagens()
        {
            this.Filtro = new ModeloDeFiltroDeHistoricoDePassagem();
            this.Lista = new List<ModeloDeHistoricoDePassagensDaLista>();
        }

        public ModeloDeListaDeHistoricoDePassagens(IEnumerable<HistoricoDePassagem> lista, List<Camera> cameras, int totalDeRegistros, ModeloDeFiltroDeHistoricoDePassagem filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeHistoricoDePassagensDaLista(a, cameras)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeHistoricoDePassagem Filtro { get; set; }
        public IList<ModeloDeHistoricoDePassagensDaLista> Lista { get; set; }
    }
}
