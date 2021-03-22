using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos
{
    public interface IServicoExternoDeAutenticacao
    {
        void Acessar(IDictionary<string, object> informacoesDoUsuario);
        void Sair();
        string PegarPerfilDoUsuarioLogado();
    }
}
