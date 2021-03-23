using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuarios.Modelos
{
    public class ModeloDeUsuarioDaLista
    {
        public ModeloDeUsuarioDaLista(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.Nome = usuario.Nome.Valor;
            this.Email = usuario.Login.Valor;
            this.DataDoCadastro = usuario.DataDoCadastro.ToShortDateString();
            this.Ativo = usuario.Ativo;
            this.PerfilDeUsuario = usuario.PerfilDeUsuario.ToString();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string DataDoCadastro { get; set; }
        public bool Ativo { get; set; }
        public string PerfilDeUsuario { get; set; }
    }
}
