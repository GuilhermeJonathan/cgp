using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao
{
    public abstract class ModeloDeLista<TEntidade, TModeloDaLista, TFiltro> where TFiltro : new() where TModeloDaLista : new() where TEntidade : class
    {
        protected ModeloDeLista()
        {
            this.Lista = new List<TModeloDaLista>();
            this.Filtro = new TFiltro();
        }

        protected ModeloDeLista(IEnumerable<TEntidade> lista, int totalDeRegistros, TFiltro filtro = default(TFiltro)) : this()
        {
            if (filtro != null)
                this.Filtro = filtro;

            this.TotalDeRegistros = totalDeRegistros;

            if (lista == null || !lista.Any())
                return;

            this.PreencherLista(lista);
        }

        public IList<TModeloDaLista> Lista { get; set; }
        public TFiltro Filtro { get; set; }
        public int TotalDeRegistros { get; set; }

        public bool TemItensNaLista => this.Lista != null && this.Lista.Any();

        private void PreencherLista(IEnumerable<TEntidade> lista)
        {
            var propriedadesDoModelo = typeof(TModeloDaLista).GetProperties();
            var propriedadesDaEntidade = typeof(TEntidade).GetProperties();

            foreach (var item in lista)
            {
                var modelo = new TModeloDaLista();

                foreach (var propriedade in propriedadesDoModelo)
                {
                    var propriedadeEncontrada = propriedadesDaEntidade.FirstOrDefault(p => p.Name == propriedade.Name);

                    if (propriedadeEncontrada == null)
                        continue;

                    try
                    {
                        propriedade.SetValue(modelo, propriedadeEncontrada.GetValue(item));
                    }
                    catch (ArgumentException ex)
                    {
                        continue;
                    }
                }

                this.Lista.Add(modelo);
            }
        }
    }
}
