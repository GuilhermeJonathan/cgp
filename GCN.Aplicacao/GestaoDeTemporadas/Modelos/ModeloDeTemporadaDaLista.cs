using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeTemporadas.Modelos
{
    public class ModeloDeTemporadaDaLista
    {
        public ModeloDeTemporadaDaLista()
        {

        }

        public ModeloDeTemporadaDaLista(Temporada temporada)
        {
            this.Id = temporada.Id;
            this.Nome = temporada.Nome;
            this.Ano = temporada.Ano;
            this.Pais = temporada.Pais;
            this.Ativo = temporada.Ativo ? "Sim" : "Não";
            this.Aberta = temporada.Aberta;
            this.DataDoCadastro = temporada.DataDoCadastro.ToShortDateString();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ano { get; set; }
        public string Pais { get; set; }
        public string Ativo { get; set; }
        public bool Aberta { get; set; }
        public string DataDoCadastro { get; set; }
    }
}
