using Campeonato.Dominio;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Infraestrutura.ServicosExternos.PersistenciaViaEntityFramework.Repositorios
{
    public class RepositorioDeFuncionarios : Repositorio<Funcionario>, IRepositorioDeFuncionarios
    {
        public RepositorioDeFuncionarios(Contexto contexto) : base(contexto) {}

        public bool VerificaSeJaFuncionario(string email)
        {
            var cliente = this._contexto.Set<Usuario>().FirstOrDefault(u => u.Login.Valor == email);
            return cliente == null ? false : true;
        }
    }
}
