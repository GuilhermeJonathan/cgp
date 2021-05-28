using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras.Modelos
{
    public class ModeloDeListaDeCameras
    {
        public ModeloDeListaDeCameras()
        {
            this.Filtro = new ModeloDeFiltroDeCamera();
            this.Lista = new List<ModeloDeCameraDaLista>();
        }

        public ModeloDeListaDeCameras(IEnumerable<Camera> lista, int totalDeRegistros, ModeloDeFiltroDeCamera filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeCameraDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeCamera Filtro { get; set; }
        public IList<ModeloDeCameraDaLista> Lista { get; set; }
    }
}
