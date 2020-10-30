using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Estadio : Entidade
    {
        public Estadio()
        {
                
        }

        public Estadio(string nome, string cidade, Time time)
        {
            this.Nome = nome;
            this.Cidade = cidade;
            this.Time = time;
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public Time Time { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }

        public void AlterarDados(string nome, string cidade, Time time, bool ativo)
        {
            this.Nome = nome;
            this.Cidade = cidade;
            this.Time = time;
            this.Ativo = ativo;
        }
    }
}
