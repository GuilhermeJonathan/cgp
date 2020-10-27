using GCN.Dominio;
using GCN.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeFuncionarios : Repositorio<Funcionario>, IRepositorioDeFuncionarios
    {
        public RepositorioDeFuncionarios(Contexto contexto) : base(contexto) {}

        public bool VerificaSeJaFuncionario(string nome)
        {
            var cliente = this._contexto.Set<Funcionario>().FirstOrDefault(u => u.Nome == nome);
            return cliente == null ? false : true;
        }
    }
}
