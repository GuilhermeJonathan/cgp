using GCN.Aplicacao.GestaoDeFuncionario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Aplicacao.GestaoDeFuncionarios
{
    public interface IServicoDeGestaoDeFuncionarios
    {
        void CadastrarNovoFuncionario(ModeloDeCadastroDeFuncionario modelo);
    }
}
