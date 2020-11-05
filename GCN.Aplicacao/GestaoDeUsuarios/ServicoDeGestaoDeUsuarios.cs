using Campeonato.Aplicacao.Comum;
using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Dominio.Entidades;
using Campeonato.Dominio.ObjetosDeValor;
using Campeonato.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campeonato.Aplicacao.GestaoDeUsuarios
{
    public class ServicoDeGestaoDeUsuarios : IServicoDeGestaoDeUsuarios
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        private readonly IServicoDeGeracaoDeHashSha _servicoDeGeracaoDeHashSha;

        public ServicoDeGestaoDeUsuarios(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia, 
            IServicoDeGeracaoDeHashSha servicoDeGeracaoDeHashSha)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
            this._servicoDeGeracaoDeHashSha = servicoDeGeracaoDeHashSha;
        }

        public string CadastrarNovoUsuario(ModeloDeCadastroDeUsuario modelo)
        {
            var usuarioComMesmoLogin = this._servicoExternoDePersistencia.RepositorioDeUsuarios.PegarAtivoPorLogin(modelo.Email);

            if (usuarioComMesmoLogin != null)
                throw new ExcecaoDeAplicacao("Já existe um usuário com o mesmo login");

            var senha = new Senha(modelo.Senha, _servicoDeGeracaoDeHashSha.GerarHash);
            var novologin = new LoginUsuario(modelo.Email);

            var novoUsuario = new Usuario(new Nome(modelo.Nome), novologin, senha);

            this._servicoExternoDePersistencia.RepositorioDeUsuarios.Inserir(novoUsuario);
            this._servicoExternoDePersistencia.Persistir();

            return "Usuário cadastrado com sucesso.";
        }

        public IList<ModeloDeUsuarioDaLista> RetonarTodosOsUsuariosAtivos()
        {
            var usuarios = this._servicoExternoDePersistencia.RepositorioDeUsuarios.RetornarTodosUsuarios();
            var modelo = new List<ModeloDeUsuarioDaLista>();

            usuarios.ToList().ForEach(a => modelo.Add(new ModeloDeUsuarioDaLista(a)));
            return modelo;
        }
    }
}
