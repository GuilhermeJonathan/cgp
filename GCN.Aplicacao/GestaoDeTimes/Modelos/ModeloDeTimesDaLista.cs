using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTimes.Modelos
{
    public class ModeloDeTimesDaLista : Modelo<Time>
    {
        public ModeloDeTimesDaLista()
        {

        }

        public ModeloDeTimesDaLista(Time time)
        {
            this.Id = time.Id;
            this.Nome = time.Nome;
            this.Imagem = time.Imagem;
            this.DataDoCadastro = time.DataDoCadastro.ToShortDateString();
            this.Ativo = time.Ativo ? "Sim" : "Não";
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Ativo { get; set; }
        public string DataDoCadastro { get; set; }

    }
}
