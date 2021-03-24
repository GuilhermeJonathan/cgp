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

        public UsuarioController(IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios, IServicoDeGestaoDeBatalhoes servicoDeGestaoDeBatalhoes)
        {
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
            this._servicoDeGestaoDeBatalhoes = servicoDeGestaoDeBatalhoes;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeUsuarios modelo)
        {
            if (!User.EhAdministrador())
                return RedirectToAction("Index", "Home");

            modelo = this._servicoDeGestaoDeUsuarios.RetonarUsuariosPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));

            modelo.Filtro.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!User.EhAdministrador())
                UsuarioSemPermissao();

            if (!id.HasValue)
                UsuarioNaoEncontrado();

            var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioPorId(id.Value);

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

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
        public ActionResult AlterarSenha()
        {
            var modelo = this._servicoDeGestaoDeUsuarios.BuscarMeusDados(User.Logado());
            return View(modelo);
        }

        [TratarErros]
        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(ModeloDeEdicaoDeUsuario modelo)
        {
            var retorno = this._servicoDeGestaoDeUsuarios.AlterarSenha(modelo);

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(MeusDados));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeUsuario modelo)
        {
            if (!User.EhAdministrador())
                UsuarioSemPermissao();

            var retorno = this._servicoDeGestaoDeUsuarios.AlterarDadosDoUsuario(modelo, User.Logado());

            modelo.Batalhoes = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<Batalhao>(nameof(Batalhao.Sigla), nameof(Batalhao.Id),
                  () => this._servicoDeGestaoDeBatalhoes.RetonarTodosOsBatalhoesAtivos());

            this.AdicionarMensagemDeSucesso(retorno);
            return RedirectToAction(nameof(Index));
        }

        [TratarErros]
        [Authorize]
        [HttpGet]
        public ActionResult Historico(int? id)
        {
            try
            {
                if (!id.HasValue)
                    UsuarioNaoEncontrado();

                var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioComHistoricoPorId(id.Value);
                this.TotalDeRegistrosEncontrados(modelo.HistoricosFinanceiros.Count);
                return View(modelo);
            } catch(Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Retiradas(ModeloDeListaDeHistoricosFinanceiros modelo)
        {
            try
            {
                modelo.Filtro.Usuarios = ListaDeItensDeDominio.DaClasseComOpcaoPadrao<ModeloDeUsuarioDaLista>(nameof(ModeloDeUsuarioDaLista.Nome), nameof(ModeloDeUsuarioDaLista.Id),
                          () => this._servicoDeGestaoDeUsuarios.RetonarTodosOsUsuariosAtivos());

                modelo = this._servicoDeGestaoDeUsuarios.BuscarHistoricosParaSaque(modelo.Filtro);
                this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
                return View(modelo);
            }
            catch (Exception ex)
            {
                this.AdicionarMensagemDeErro(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public JsonResult BuscarUsuario(int idUsuario)
        {
            var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioPorId(idUsuario);
            return Json(new { nome = modelo.Nome}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AtivarUsuario(int id)
        {
            var modelo = this._servicoDeGestaoDeUsuarios.AtivarUsuario(id, User.Logado());
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