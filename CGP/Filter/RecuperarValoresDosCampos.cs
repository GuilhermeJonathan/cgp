using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cgp.Filter
{
    public sealed class RecuperarValoresDosCampos : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = (ModelStateDictionary)filterContext.Controller.TempData["_modelState"];
            if (modelState == null)
                return;

            filterContext.Controller.ViewData.ModelState.Merge(modelState);
        }
    }
}