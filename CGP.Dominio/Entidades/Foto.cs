using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Foto : Entidade
    {
        public Foto()
        {

        }

        public Foto(Carater Carater, string descricao, string caminho)
        {
            this.Carater = Carater;
            this.Descricao = descricao;
            this.Caminho = caminho;
            this.Ativo = true;
        }

        public string Descricao { get; set; }
        public string Caminho { get; set; }
        public bool Ativo { get; set; }
        public Carater Carater { get; set; }

        public void InativarFoto()
        {
            this.Ativo = false;
        }
    }
}
