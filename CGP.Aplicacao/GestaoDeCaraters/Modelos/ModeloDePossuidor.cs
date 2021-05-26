using Cgp.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCaraters.Modelos
{
    public class ModeloDePossuidor
    {
        public ModeloDePossuidor(Possuidor possuidor)
        {
            this.Nome = possuidor.Nome;
            this.Documento = possuidor.Documento;
            this.TipoDocumento = possuidor.TipoDocumento;
            this.Endereco = possuidor.Endereco;
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Endereco { get; set; }
    }
}
