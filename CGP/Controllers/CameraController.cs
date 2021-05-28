using Cgp.Aplicacao.GestaoDeCameras;
using Cgp.Aplicacao.GestaoDeCameras.Modelos;
using Cgp.Aplicacao.GestaoDeCidades;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Filter;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    [Authorize]
    [TratarErros]
    public class CameraController : Controller
    {
        
        private readonly IServicoDeGestaoDeCidades _servicoDeGestaoDeCidades;
        private readonly IServicoDeGestaDeCameras _servicoDeGestaoDeCameras;
        
        public CameraController(IServicoDeGestaoDeCidades servicoDeGestaoDeCidades, IServicoDeGestaDeCameras servicoDeGestaoDeCameras)
        {
            this._servicoDeGestaoDeCidades = servicoDeGestaoDeCidades;
            this._servicoDeGestaoDeCameras = servicoDeGestaoDeCameras;
        }


        public ActionResult Index(ModeloDeListaDeCameras modelo)
        {
            modelo = this._servicoDeGestaoDeCameras.RetonarCamerasPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
              () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            return View(modelo);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var modelo = new ModeloDeCadastroDeCamera();

            modelo.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
              () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Cadastrar(ModeloDeCadastroDeCamera modelo)
        {
            var retorno = this._servicoDeGestaoDeCameras.CadastrarCamera(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!User.EhAdministrador() && !User.EhInterno())
                return UsuarioSemPermissao();

            if (!id.HasValue)
                return CameraNaoEncontrada();

            var modelo = this._servicoDeGestaoDeCameras.BuscarCaraterPorId(id.Value, User.Logado());

            modelo.Cidades = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Cidade>(nameof(Cidade.Descricao), nameof(Cidade.Id),
               () => this._servicoDeGestaoDeCidades.RetonarCidadesPorUf(7));

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeCamera modelo)
        {
            var retorno = this._servicoDeGestaoDeCameras.AlterarDadosDaCamera(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AtivarCamera(int id)
        {
            var modelo = this._servicoDeGestaoDeCameras.AtivarCamera(id, User.Logado());
            return Content(modelo);
        }

        private ActionResult CameraNaoEncontrada()
        {
            this.AdicionarMensagemDeErro("A câmera não foi encontrada");
            return RedirectToAction(nameof(Index));
        }

        private ActionResult UsuarioSemPermissao()
        {
            this.AdicionarMensagemDeErro("Usuário sem permissão para esta funcionalidade.");
            return RedirectToAction("Index", "Home");
        }
    }
}