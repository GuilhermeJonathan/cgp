using Cgp.Aplicacao.GestaoDeCameras.Modelos;
using Cgp.Dominio.Entidades;
using Cgp.Dominio.ObjetosDeValor;
using Cgp.Infraestrutura.InterfaceDeServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cgp.Aplicacao.GestaoDeCameras
{
    public class ServicoDeGestaoDeCameras : IServicoDeGestaDeCameras
    {
        private readonly IServicoExternoDePersistenciaViaEntityFramework _servicoExternoDePersistencia;
        public ServicoDeGestaoDeCameras(IServicoExternoDePersistenciaViaEntityFramework servicoExternoDePersistencia)
        {
            this._servicoExternoDePersistencia = servicoExternoDePersistencia;
        }

        public ModeloDeListaDeCameras RetonarCamerasPorFiltro(ModeloDeFiltroDeCamera filtro, int pagina, int registrosPorPagina = 30)
        {
            try
            {
                var quantidadeEncontrada = 0;
                var cameras = this._servicoExternoDePersistencia.RepositorioDeCameras.RetornarCamerasPorFiltro(filtro.Nome, 
                    filtro.Cidade, filtro.Ativo, out quantidadeEncontrada);

                return new ModeloDeListaDeCameras(cameras, quantidadeEncontrada, filtro);
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar os crimes");
            }
        }

        public Camera BuscarCameraPorEndereco(string endereco)
        {
            try
            {
                var camera = this._servicoExternoDePersistencia.RepositorioDeCameras.PegarPorEndereco(endereco);
                if (camera == null)
                    new ExcecaoDeAplicacao("Câmera não encontrada.");

                return camera;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar camêra");
            }
        }

        public string CadastrarCamera(ModeloDeCadastroDeCamera modelo, UsuarioLogado usuario)
        {
            try
            {
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);

                var novaCamera = new Camera(modelo.Ponto, modelo.Nome, modelo.Latitude, modelo.Longitude, cidade, usuarioBanco);
                this._servicoExternoDePersistencia.RepositorioDeCameras.Inserir(novaCamera);
                this._servicoExternoDePersistencia.Persistir();

                return "Câmera incluída com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível incluir a câmera: " + ex.InnerException);
            }
        }

        public ModeloDeEdicaoDeCamera BuscarCaraterPorId(int id, UsuarioLogado usuario)
        {
            try
            {
                var camera = this._servicoExternoDePersistencia.RepositorioDeCameras.PegarPorId(id);
                var modelo = new ModeloDeEdicaoDeCamera(camera);
                return modelo;
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Erro ao consultar câmera");
            }
        }

        public string AlterarDadosDaCamera(ModeloDeEdicaoDeCamera modelo, UsuarioLogado usuario)
        {
            try
            {
                var camera = this._servicoExternoDePersistencia.RepositorioDeCameras.PegarPorId(modelo.Id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);
                var cidade = this._servicoExternoDePersistencia.RepositorioDeCidades.PegarPorId(modelo.Cidade);

                camera.AlterarDados(modelo.Ponto, modelo.Nome, modelo.Latitude, modelo.Longitude, cidade, modelo.Ativo, usuarioBanco);

                this._servicoExternoDePersistencia.Persistir();

                return "Câmera alterada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar a Câmera: " + ex.InnerException);
            }
        }

        public string AtivarCamera(int id, UsuarioLogado usuario)
        {
            try
            {
                var camera = this._servicoExternoDePersistencia.RepositorioDeCameras.PegarPorId(id);
                var usuarioBanco = this._servicoExternoDePersistencia.RepositorioDeUsuarios.BuscarPorId(usuario.Id);

                if (camera != null)
                {
                    if (camera.Ativo)
                        camera.Inativar(usuarioBanco);
                    else
                        camera.Ativar(usuarioBanco);
                }

                this._servicoExternoDePersistencia.Persistir();

                return "Câmera alterada com sucesso.";
            }
            catch (Exception ex)
            {
                throw new ExcecaoDeAplicacao("Não foi possível alterar a Câmera: " + ex.InnerException);
            }
        }
    }
}
