using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Dominio.Repositorios
{
    public interface IRepositorioDeClientes : IRepositorio<Cliente>
    {
        bool VerificaSeJaCliente(string nome);
    }
}
