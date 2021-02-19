using Campeonato.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Campeonato.Aplicacao.GestaoDePremiacoes.Modelos
{
    public class ModeloDeCadastroDePremiacao
    {
        public ModeloDeCadastroDePremiacao()
        {
            this.UsuariosPrimeiro = new List<SelectListItem>();
            this.UsuariosSegundo = new List<SelectListItem>();
            this.Rodadas = new List<SelectListItem>();
        }

        public int Rodada { get; set; }
        public IEnumerable<SelectListItem> Rodadas { get; set; }
        public int UsuarioPrimeiro { get; set; }
        public IEnumerable<SelectListItem> UsuariosPrimeiro { get; set; }
        public int UsuarioSegundo { get; set; }
        public IEnumerable<SelectListItem> UsuariosSegundo { get; set; }
        public string ValorTotal { get; set; }
        public string ValorPremiacaoPrimeiro { get; set; }
        public string ValorPremiacaoSegundo { get; set; }
        public string ValorAdministracao { get; set; }
        public string ValorAcumulado { get; set; }
    }
}
