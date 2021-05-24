using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDeProprietarioDaLista
    {
        public ModeloDeProprietarioDaLista(Proprietario proprietario)
        {
            this.Nome = proprietario.nomeProprietario;
            this.Documento = proprietario.numeroDocumentoProprietario;
            this.TipoDocumento = proprietario.tipoDocumentoProprietario;
            this.Endereco = proprietario.enderecoProprietario;
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Endereco { get; set; }

    }
}
