using Cgp.Processos.Interface;
using Cgp.Processos.Servicos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cgp.Processos
{
    class Program
    {

        static void Main(string[] args)
        {
            ConfigurarGlobalizacaoParaPortugues();

            using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["conexaoDoBanco"].ConnectionString))
            {
                IProcessarTarefas processar = new ProcessarTarefas(conexao);

                var tarefas = new List<Task>();
                tarefas.Add(processar.Inicializar());
                Task.WaitAll(tarefas.ToArray());
            }
        }

        private static void ConfigurarGlobalizacaoParaPortugues()
        {
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
