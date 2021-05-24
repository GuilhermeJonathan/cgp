using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDeListaDeBuscas
    {
        public ModeloDeListaDeBuscas()
        {
            this.Filtro = new ModeloDeFiltroDeBusca();
            this.Lista = new List<ModeloDeBuscaDaLista>();
        }

        public ModeloDeListaDeBuscas(IEnumerable<ModeloDeBuscaCompleto> lista, int totalDeRegistros, ModeloDeFiltroDeBusca filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeBuscaDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeBusca Filtro { get; set; }
        public IList<ModeloDeBuscaDaLista> Lista { get; set; }
    }
}
