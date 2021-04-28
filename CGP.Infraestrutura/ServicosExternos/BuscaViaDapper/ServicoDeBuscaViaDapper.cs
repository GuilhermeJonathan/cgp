using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace Cgp.Infraestrutura.ServicosExternos.BuscaViaDapper
{
    public class ServicoDeBuscaViaDapper : IServicoDeBuscaViaDapper
    {
        private readonly string _stringDeConexao;
        public ServicoDeBuscaViaDapper(string stringDeConexao)
        {
            this._stringDeConexao = stringDeConexao;
        }

        public IEnumerable<T> Buscar<T>(string query, object parametros = null)
        {
            using (var conexao = new SqlConnection(this._stringDeConexao))
            {
                if (parametros == null)
                    return conexao.Query<T>(query, commandTimeout: 1000);
                else
                    return conexao.Query<T>(query, parametros, commandTimeout: 1000);
            }
        }

        public void Salvar(string query, object parametros = null)
        {
            using (var conexao = new SqlConnection(this._stringDeConexao))
            {
                if (parametros == null)
                    conexao.Execute(query, commandTimeout: 1000);
                else
                    conexao.Execute(query, parametros, commandTimeout: 1000);
            }
        }
    }
}
