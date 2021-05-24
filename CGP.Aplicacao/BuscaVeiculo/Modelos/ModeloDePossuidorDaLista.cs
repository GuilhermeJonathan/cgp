using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo.Modelos
{
    public class ModeloDePossuidorDaLista
    {
        public ModeloDePossuidorDaLista(Possuidor possuidor)
        {
            this.Nome = possuidor.nomePossuidor;
            this.Documento = possuidor.numeroDocumentoPossuidor;
            this.TipoDocumento = possuidor.tipoDocumentoPossuidor;
            this.Endereco = possuidor.enderecoPossuidor;
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Endereco { get; set; }
    }
}
