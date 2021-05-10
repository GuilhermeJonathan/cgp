using Cgp.Aplicacao.BuscaVeiculo;
using Cgp.Aplicacao.GestaoDeBatalhoes;
using Cgp.Aplicacao.GestaoDeUsuarios;
using Cgp.Aplicacao.GestaoDeUsuarios.Modelos;
using Cgp.Aplicacao.Util;
using Cgp.CustomExtensions;
using Cgp.Dominio.Entidades;
using Cgp.Filter;
using Cgp.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Controllers
{
    [TratarErros]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;
        private readonly IServicoDeGestaoDeBatalhoes _servicoDeGestaoDeBatalhoes;
        private readonly IServicoDeBuscaDeVeiculo _servicoDeGestaoDeVeiculos;

        public UsuarioController(IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios, IServicoDeGestaoDeBatalhoes servicoDeGestaoDeBatalhoes, IServicoDeBuscaDeVeiculo servicoDeGestaoDeVeiculos)
        {
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
            this._servicoDeGestaoDeBatalhoes = servicoDeGestaoDeBatalhoes;
            this._servicoDeGestaoDeVeiculos = servicoDeGestaoDeVeiculos;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeUsuarios modelo, bool? Ativo)
        {
            if (User.EhInterno() && User.EhUsuario())
                return UsuarioSemPermissao();

            if (Ativo.HasValue)
                modelo.Filtro.Ativo = Ativo.Value;

            modelo = this._servicoDeGestaoDeUsuarios.RetonarUsuariosPorFiltro(modelo.Filtro, User.Logado(),  this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());
            
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (User.EhInterno() && User.EhUsuario())
                return UsuarioSemPermissao();

            if (!id.HasValue)
                return UsuarioNaoEncontrado();

            var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioPorId(id.Value);

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

            if(User.EhAtenas())
                return View(nameof(EditarPerfil), modelo);

            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MeusDados()
        {
            var modelo = this._servicoDeGestaoDeUsuarios.BuscarMeusDados(User.Logado());

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

            return View(modelo);
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult MeusDados(ModeloDeEdicaoDeUsuario modelo)
        {
            var retorno = this._servicoDeGestaoDeUsuarios.EditarMeusDados(modelo, User.Logado());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(MeusDados));
        }

        [Authorize]
        [HttpGet]
        public ActionResult AlterarSenha(int? id)
        {
            if (User.EhInterno() && User.EhUsuario())
                return UsuarioSemPermissao();

            var idUsuario = User.Logado().Id;

            if (id.HasValue)
                idUsuario = id.Value;

            var modelo = this._servicoDeGestaoDeUsuarios.BuscarMeusDadosParaAlterarSenha(idUsuario);
            return View(modelo);
        }

        [TratarErros]
        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(ModeloDeEdicaoDeUsuario modelo)
        {
            if (User.EhInterno() && User.EhUsuario())
                return UsuarioSemPermissao();

            var retorno = this._servicoDeGestaoDeUsuarios.AlterarSenha(modelo, User.Logado());
            this.AdicionarMensagemDeSucesso(retorno.Item1);
            if (retorno.Item2)
                return RedirectToAction(nameof(MeusDados));
            else
                return RedirectToAction(nameof(Editar), new { id = modelo.Id });
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeUsuario modelo)
        {
            if (User.EhInterno() && User.EhUsuario())
                return UsuarioSemPermissao();

            var retorno = this._servicoDeGestaoDeUsuarios.AlterarDadosDoUsuario(modelo, User.Logado());

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditarPerfil(ModeloDeEdicaoDeUsuario modelo)
        {
            if (User.EhInterno() && User.EhUsuario())
                return UsuarioSemPermissao();

            var retorno = this._servicoDeGestaoDeUsuarios.AlterarPerfilDoUsuario(modelo, User.Logado());

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public JsonResult BuscarUsuario(int idUsuario)
        {
            var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioPorId(idUsuario);
            return Json(new { nome = modelo.Nome}, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AtivarUsuario(int id)
        {
            var modelo = await this._servicoDeGestaoDeUsuarios.AtivarUsuario(id, User.Logado());
            return Content(modelo);
        }

        [HttpGet]
        public JsonResult QuantidadeDeUsuariosNovos()
        {
            var quantidade = this._servicoDeGestaoDeUsuarios.BuscarUsuariosNovos();
            return Json(new { quantidade }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult UsuarioNaoEncontrado()
        {
            this.AdicionarMensagemDeErro("Usuário não encontrado");
            return RedirectToAction(nameof(Index));
        }

        private ActionResult UsuarioSemPermissao()
        {
            this.AdicionarMensagemDeErro("Usuário sem permissão para esta funcionalidade.");
            return RedirectToAction("Index", "Home");
        }
    }
}