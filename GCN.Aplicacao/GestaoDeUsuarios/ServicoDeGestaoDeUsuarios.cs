using GCN.Aplicacao.Comum;
using GCN.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCN.Aplicacao.GestaoDeUsuarios
{
    public class ServicoDeGestaoDeUsuarios : IServicoDeGestaoDeUsuarios
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeUsuarios(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public string CadastrarNovoUsuario()
        {
            try
            {

                var usuarioComMesmoLogin = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarAtivoPorLogin("teste@gmail.com");

                if (usuarioComMesmoLogin != null)
                    throw new ExcecaoDeAplicacao("Já existe um usuário com o mesmo login");

            } catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao cadastrar usuário");
            }

            return "Usuário cadastrado com sucesso.";
        }
    }
}
