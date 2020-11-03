using Campeonato.Aplicacao.Comum;
using Campeonato.Aplicacao.Login.Modelos;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using Campeonato.Infraestrutura.ServicosExternos.InterfacesDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.Login
{
    public class ServicoDeLogin : IServicoDeLogin
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoExternoDeAutenticacao _servicoDeAutenticacao;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;
        

        public ServicoDeLogin(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, IServicoExternoDeAutenticacao servicoDeAutenticacao,
            IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha)
        {
            _servicoExternoDePersistencia = servicoExternoDePersistencia;
            _servicoDeAutenticacao = servicoDeAutenticacao;
            _servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
        }

        public void Entrar(ModeloDeLogin modelo)
        {
            var usuario = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarPorLoginESenha(modelo.Login, modelo.SenhaCriptograda(this._servicoDeGeracaoDeHashSha.GerarHash));

            if(usuario == null)
                throw new ExcecaoDeAplicacao("Usuário e/ou senha inválidos");

            var dadosDaSessao = new Dictionary<string, object>
                {
                    { "id", usuario.Id },
                    { "nome", usuario.Nome.Valor },
                    { "login", usuario.Login.Valor },
                    { "perfil", usuario.PerfilDeUsuario },
                    { "dataDoCadastro", usuario.DataDoCadastro }
                };

            this._servicoDeAutenticacao.Acessar(dadosDaSessao);
        }

        public void Sair()
        {
            this._servicoDeAutenticacao.Sair();
        }
    }
}
