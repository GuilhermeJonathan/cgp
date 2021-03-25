using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCrimes.Modelos
{
    public class ModeloDeEdicaoDeCrime
    {
        public ModeloDeEdicaoDeCrime()
        {

        }

        public ModeloDeEdicaoDeCrime(Crime crime)
        {
            this.Id = crime.Id;
            this.Artigo = crime.Artigo;
            this.Nome = crime.Nome;
            this.Ativo = crime.Ativo;
            this.DataDoCadastro = crime.DataDoCadastro.ToShortDateString();
        }

        public int Id { get; set; }
        public string Artigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string DataDoCadastro { get; set; }
    }
}
