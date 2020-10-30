using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Dominio.Entidades
{
    public class Time : Entidade
    {
        public Time()
        {

        }

        public Time(string nome, string imagem)
        {
            this.Nome = nome;
            this.Imagem = imagem;
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Imagem { get; set; }
        public bool Ativo { get; set; }

        public void AlterarDados(string nome, bool ativo)
        {
            this.Nome = nome;
            this.Ativo = ativo;
        }
    }
}
