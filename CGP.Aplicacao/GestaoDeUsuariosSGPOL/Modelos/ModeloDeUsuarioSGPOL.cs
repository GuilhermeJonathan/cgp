using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeUsuariosSGPOL.Modelos
{
    public class ModeloDeUsuarioSGPOL
    {
        public ModeloDeUsuarioSGPOL() {}

        public int codigo { get; set; }
        public string nome { get; set; }
        public string dataNascimento { get; set; }
        public string matricula { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string posto { get; set; }
        public string quadro { get; set; }
        public string lotacao { get; set; }
        public int lotacaoCodigo { get; set; }
        public int lotacaoCodigoSubordinacao { get; set; }
        public string nomeGuerra { get; set; }
        public string nomePai { get; set; }
        public string nomeMae { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string orgaoexpedidorrg { get; set; }
        public string telefonefixo { get; set; }
        public string celular { get; set; }
        public string dataAdmissao { get; set; }
        public string situacaoFuncional { get; set; }
        public object dataDesligamento { get; set; }
        public string previsaoFerias { get; set; }
        public string siape { get; set; }
        public int restricaoMedica { get; set; }
        public string email { get; set; }
        public Cnh cnh { get; set; }
    }
}
