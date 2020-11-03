using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeUsuarioDaLista
    {
        public ModeloDeUsuarioDaLista(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.Nome = usuario.Nome.Valor;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
