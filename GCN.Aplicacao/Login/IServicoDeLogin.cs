using Campeonato.Aplicacao.Login.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.Login
{
    public interface IServicoDeLogin
    {
        void Entrar(ModeloDeLogin modelo);
        void Sair();
    }
}
