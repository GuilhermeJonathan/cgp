using Cgp.Aplicacao.GestaoDeFuncionario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeFuncionarios
{
    public interface IServicoDeGestaoDeFuncionarios
    {
        void CadastrarNovoFuncionario(ModeloDeCadastroDeFuncionario modelo);
    }
}
