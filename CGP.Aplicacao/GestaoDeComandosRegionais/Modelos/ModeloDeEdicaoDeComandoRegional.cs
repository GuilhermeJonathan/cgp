using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeComandosRegionais.Modelos
{
    public class ModeloDeEdicaoDeComandoRegional
    {
        public ModeloDeEdicaoDeComandoRegional()
        {

        }

        public ModeloDeEdicaoDeComandoRegional(ComandoRegional comando)
        {
            this.Id = comando.Id;
            this.Nome = comando.Nome;
            this.Sigla = comando.Sigla;
            this.Ativo = comando.Ativo;
            this.UsuarioAlterou = comando.UsuarioQueAlterou != null ? comando.UsuarioQueAlterou.Nome.Valor : String.Empty;
            this.DataDoCadastro = comando.DataDoCadastro.ToShortDateString();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string UsuarioAlterou { get; set; }
        public bool Ativo { get; set; }
        public string DataDoCadastro { get; set; }

    }
}
