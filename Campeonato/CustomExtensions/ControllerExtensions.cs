using Campeonato.Aplicacao.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.CustomExtensions
{
    public static class ControllerExtensions
    {
        public static void AdicionarMensagemDeSucesso(this ControllerBase controller, string mensagem)
        {
            mensagem = mensagem.ConfigurarMensagemParaNaoTerCaracteresEstranhos();
            controller.TempData["MensagemDeSucesso"] = mensagem;
        }

        public static void AdicionarMensagemDeErro(this ControllerBase controller, string mensagem)
        {
            mensagem = mensagem.ConfigurarMensagemParaNaoTerCaracteresEstranhos();
            controller.TempData["MensagemDeErro"] = mensagem;
        }

        public static int Pagina(this ControllerBase controller)
        {
            var pagina = controller.ControllerContext.HttpContext.Request.QueryString["grid-page"] != null
                ? int.Parse(controller.ControllerContext.HttpContext.Request.QueryString["grid-page"]) : 1;

            return pagina > 0 ? pagina : 1;
        }

        public static void TotalDeRegistrosEncontrados(this ControllerBase controller, int totalDeRegistros)
        {
            controller.ViewBag.TotalDeRegistros = totalDeRegistros;
        }
    }
}