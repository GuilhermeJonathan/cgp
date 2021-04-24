using Cgp.Aplicacao.BuscaVeiculo.Modelos;
using Cgp.Aplicacao.BuscaVeiculo.ModelosCortex;
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
        Task<ModeloDeBuscaCompleto> BuscarPlacaComleta(string placa);
    }
}
