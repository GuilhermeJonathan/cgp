using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace Campeonato.Filter
{
    public class ViewModelMenuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var viewBag = filterContext.Controller.ViewBag;
            var controller = filterContext.RouteData.Values["controller"] as string;
            viewBag.Controller = controller;

        }
    }
}