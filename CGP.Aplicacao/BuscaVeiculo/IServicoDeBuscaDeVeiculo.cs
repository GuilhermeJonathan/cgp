using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
using Cgp.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.BuscaVeiculo
{
    public interface IServicoDeBuscaDeVeiculo
    {
        Task<ModeloDeBuscaDeVeiculo> BuscarPlacaSimples(string placa);
        Task<ModeloDeBuscaCompleto> BuscarPlacaCompleta(string placa, UsuarioLogado usuario);
        Task<ModeloDeListaDeBuscas> BuscarPlacasPorFiltro(ModeloDeFiltroDeBusca filtro, UsuarioLogado usuario);
        Task<ModeloDeBuscaDaLista> DetalharVeiculo(ModeloDeFiltroDeBusca filtro, UsuarioLogado usuario);
    }
}
