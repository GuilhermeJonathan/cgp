using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos
{
    public interface IServicoExternoDeAutenticacao
    {
        void Acessar(IDictionary<string, object> informacoesDoUsuario);
        void Sair();
        int PegarEmpresaDoUsuarioLogado();
        string PegarPerfilDoUsuarioLogado();
    }
}
