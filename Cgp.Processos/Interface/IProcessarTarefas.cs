using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Processos.Interface
{
    public interface IProcessarTarefas
    {
        Task<string> Inicializar();
    }
}
