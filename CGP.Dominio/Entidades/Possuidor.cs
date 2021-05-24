using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Dominio.Entidades
{
    public class Possuidor : Entidade
    {
        public Possuidor()
        {

        }

        public Possuidor(string nome, string documento, string endereco, string tipoDocumento)
        {
            this.Nome = nome;
            this.Documento = documento;
            this.Endereco = endereco;
            this.TipoDocumento = tipoDocumento;
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Endereco { get; set; }
        public string TipoDocumento { get; set; }
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
