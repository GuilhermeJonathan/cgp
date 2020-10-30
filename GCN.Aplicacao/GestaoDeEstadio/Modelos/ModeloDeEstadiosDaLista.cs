using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeEstadio.Modelos
{
    public class ModeloDeEstadiosDaLista : Modelo<Estadio>
    {
        public ModeloDeEstadiosDaLista()
        {

        }

        public ModeloDeEstadiosDaLista(Estadio estadio)
        {
            this.Id = estadio.Id;
            this.Nome = estadio.Nome;
            this.Cidade = estadio.Cidade;
            this.ImagemTime = estadio.Time != null ? estadio.Time.Imagem : "";
            this.DataDoCadastro = estadio.DataDoCadastro.ToShortDateString();
            this.Time = estadio.Time;
            this.Ativo = estadio.Ativo ? "Sim" : "Não";
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Ativo { get; set; }
        public string ImagemTime { get; set; }
        public string DataDoCadastro { get; set; }
        public Time Time { get; set; }
    }
}
