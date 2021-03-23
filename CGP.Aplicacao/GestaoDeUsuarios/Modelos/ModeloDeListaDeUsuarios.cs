using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeListaDeUsuarios
    {
        public ModeloDeListaDeUsuarios()
        {
            this.Filtro = new ModeloDeFiltroDeUsuario();
            this.Lista = new List<ModeloDeUsuarioDaLista>();
        }

        public ModeloDeListaDeUsuarios(IEnumerable<Usuario> lista, int totalDeRegistros, ModeloDeFiltroDeUsuario filtro = null) : this()
        {
            if (lista == null)
                return;

            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;
            lista.ToList().ForEach(a => this.Lista.Add(new ModeloDeUsuarioDaLista(a)));
        }

        public int TotalDeRegistros { get; set; }
        public ModeloDeFiltroDeUsuario Filtro { get; set; }
        public IList<ModeloDeUsuarioDaLista> Lista { get; set; }
    }
}
