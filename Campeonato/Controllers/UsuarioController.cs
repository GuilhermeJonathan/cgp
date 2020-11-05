using Campeonato.Aplicacao.GestaoDeUsuarios;
using Campeonato.Aplicacao.GestaoDeUsuarios.Modelos;
using Campeonato.Aplicacao.Util;
using Campeonato.CustomExtensions;
using Campeonato.Filter;
using Campeonato.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IServicoDeGestaoDeUsuarios _servicoDeGestaoDeUsuarios;

        public UsuarioController(IServicoDeGestaoDeUsuarios servicoDeGestaoDeUsuarios)
        {
            this._servicoDeGestaoDeUsuarios = servicoDeGestaoDeUsuarios;
        }

        [HttpGet]
        public ActionResult Index(ModeloDeListaDeUsuarios modelo)
        {
            modelo = this._servicoDeGestaoDeUsuarios.RetonarUsuariosPorFiltro(modelo.Filtro, this.Pagina(), VariaveisDeAmbiente.Pegar<int>("registrosPorPagina"));
            this.TotalDeRegistrosEncontrados(modelo.TotalDeRegistros);
            return View(modelo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (!id.HasValue)
                UsuarioNaoEncontrado();

            var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioPorId(id.Value);

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Editar(ModeloDeEdicaoDeUsuario modelo)
        {
            var retorno = this._servicoDeGestaoDeUsuarios.AlterarDadosDoUsuario(modelo, User.Logado());

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

        [HttpGet]
        public JsonResult BuscarUsuario(int idUsuario)
        {
            var modelo = this._servicoDeGestaoDeUsuarios.BuscarUsuarioPorId(idUsuario);
            return Json(new { nome = modelo.Nome, credito = modelo.Credito }, JsonRequestBehavior.AllowGet);
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
    }
}