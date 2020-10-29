using Campeonato.Aplicacao.GestaoDeFuncionario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeFuncionarios
{
    public interface IServicoDeGestaoDeFuncionarios
    {
        void CadastrarNovoFuncionario(ModeloDeCadastroDeFuncionario modelo);
    }
}
