using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDeProprietario
    {
        public ModeloDeProprietario(Proprietario proprietario)
        {
            this.Nome = proprietario.Nome;
            this.Documento = proprietario.Documento;
            this.TipoDocumento = proprietario.TipoDocumento;
            this.Endereco = proprietario.Endereco;
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Endereco { get; set; }
    }
}
