using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class HistoricoDeCarater : Entidade
    {
        public HistoricoDeCarater()
        {

        }

        public HistoricoDeCarater(string titulo, string descricao, TipoDeHistoricoDeCarater tipoDeHistoricoDeCarater, Usuario usuario, int idEntidade)
        {
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.TipoDeHistoricoDeCarater = tipoDeHistoricoDeCarater;
            this.Usuario = usuario;
            this.IdEntidade = idEntidade;
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Usuario Usuario { get; set; }
        public Carater Carater { get; set; }
        public TipoDeHistoricoDeCarater TipoDeHistoricoDeCarater { get; set; }
        public int IdEntidade { get; set; }
        public bool Excluido { get; set; } = false;

        public void Excluir()
        {
            this.Excluido = true;
        }
    }
}
