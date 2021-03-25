using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCrimes.Modelos
{
    public class ModeloDeCrimesDaLista
    {
        public ModeloDeCrimesDaLista()
        {

        }

        public ModeloDeCrimesDaLista(Crime crime)
        {
            if (crime == null)
                return;

            this.Id = crime.Id;
            this.Artigo = crime.Artigo;
            this.Nome = crime.Nome;
            this.Ativo = crime.Ativo;
        }

        public int Id { get; set; }
        public string Artigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
