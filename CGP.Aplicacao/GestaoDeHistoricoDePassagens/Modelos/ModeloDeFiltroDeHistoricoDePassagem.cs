using Cgp.Aplicacao.Util;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cgp.Aplicacao.GestaoDeHistoricoDePassagens.Modelos
{
    public class ModeloDeFiltroDeHistoricoDePassagem
    {
        public ModeloDeFiltroDeHistoricoDePassagem()
        {
            this.Cidades = new List<SelectListItem>();
            this.SituacoesDoCarater = ListaDeItensDeDominio.DoEnumComOpcaoPadrao<SituacaoDoCarater>(ignorarEnum: new string[] { "Excluído" });
        }

        public string Placa { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int Cidade { get; set; }
        public int[] CidadesSelecionadas { get; set; }
        public IEnumerable<SelectListItem> Cidades { get; set; }
        public int Crime { get; set; }
        public int[] CrimesSelecionados { get; set; }
        public IEnumerable<SelectListItem> Crimes { get; set; }
        public int SituacaoDoCarater { get; set; }
        public IEnumerable<SelectListItem> SituacoesDoCarater { get; set; }
    }
}
