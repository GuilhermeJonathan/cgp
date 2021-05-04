
using Cgp.Aplicacao.Processos.InterfaceDeServicos;
using Cgp.Aplicacao.Processos.Servicos;
using Cgp.Processos.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Processos.Servicos
{
    public class ProcessarTarefas : IProcessarTarefas
    {
        private readonly IServicoDeBuscaDeCaraters _servicoDeBuscaDeCaraters;

        public ProcessarTarefas(SqlConnection conexao)
        {
            this._servicoDeBuscaDeCaraters = new ServicoDeBuscaDeCaraters(conexao);
        }

        public async Task<string> Inicializar()
        {
            try
            {
                await this._servicoDeBuscaDeCaraters.Executar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "Sucesso";
        }
    }
}
