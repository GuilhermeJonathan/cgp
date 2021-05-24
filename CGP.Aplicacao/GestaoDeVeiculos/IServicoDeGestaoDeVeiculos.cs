using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeVeiculos
{
    public interface IServicoDeGestaoDeVeiculos
    {
        string CadastrarProprietarioPossuidor(ModeloDeBuscaDaLista modelo, UsuarioLogado usuario);
    }
}
