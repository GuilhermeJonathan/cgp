using Campeonato.Aplicacao;
using Campeonato.CustomExtensions;
using Campeonato.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campeonato.Filter
{
    public sealed class TratarErros : FilterAttribute, IExceptionFilter
    {
        public string RedirectAction { get; set; }
        public string RedirectController { get; set; }

        public void OnException(ExceptionContext contexto)
        {
            if (contexto.Exception == null)
                return;

            var f = contexto.Controller.ViewData.ModelState;

            this.TratarExcecao(contexto, typeof(HttpRequestValidationException), "Um ou mais campos do formulário contém caracteres especiais inválidos. Ex: < >");
            this.TratarExcecao(contexto, typeof(ExcecaoDeAplicacao));
            this.TratarExcecao(contexto, typeof(ExcecaoDeNegocio));
            this.TratarExcecao(contexto, typeof(InvalidOperationException));
        }

        private void TratarExcecao(ExceptionContext contexto, Type tipoDeExecaoEsperado, string mensagemDeErro = "")
        {
            if (contexto.Exception.GetType() != tipoDeExecaoEsperado)
                return;

            GravarEstadoDoFormulario(contexto);

            if (!string.IsNullOrEmpty(mensagemDeErro))
                contexto.Controller.AdicionarMensagemDeErro(mensagemDeErro);
            else
                contexto.Controller.AdicionarMensagemDeErro(contexto.Exception.Message);

            this.MarcarExcecaoComoTratada(contexto);
            this.Redirecionar(contexto);
        }

        private void MarcarExcecaoComoTratada(ExceptionContext contexto)
        {
            contexto.ExceptionHandled = true;
        }

        private void GravarEstadoDoFormulario(ExceptionContext contexto)
        {
            contexto.Controller.TempData["_modelState"] = contexto.Controller.ViewData.ModelState;
        }

        private void Redirecionar(ExceptionContext contexto)
        {
            if (!string.IsNullOrEmpty(this.RedirectAction))
            {
                var controller = !string.IsNullOrEmpty(this.RedirectController) ? this.RedirectController : contexto.RouteData.Values["controller"];

                contexto.Result = new RedirectResult($"/{controller}/{this.RedirectAction}");
            }
            else
                contexto.Result = new RedirectResult(contexto.HttpContext.Request.UrlReferrer != null
                    ? contexto.HttpContext.Request.UrlReferrer.LocalPath
                    : contexto.HttpContext.Request.Path);
        }
    }
}