using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTimes.Modelos
{
    public class ModeloDeEdicaoDeTime
    {
        public ModeloDeEdicaoDeTime()
        {
                
        }

        public ModeloDeEdicaoDeTime(Time time)
        {
            this.Id = time.Id;
            this.Nome = time.Nome;
            this.Ativo = time.Ativo;
            this.DataDoCadastro = time.DataDoCadastro.ToShortDateString();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string DataDoCadastro { get; set; }
    }
}
