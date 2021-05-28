using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Cgp.CustomExtensions
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString InputTextFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", int maxLength = short.MaxValue, int minLength = 0, bool required = true, string id = "", string placeholder = "",
            bool disabled = false, bool @readonly = false, string @class = "", bool @exibeTitulo = true)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            
            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class, id, required, disabled, @readonly, placeholder,
                minLength, maxLength, exibeTitulo));
        }

        public static MvcHtmlString InputTextFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            dynamic properties)// int maxLength = 255, int minLength = 0, bool required = true, string id = "", string placeholder = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            return new MvcHtmlString(Input("text", name, metadata.Model as string, properties));
        }

        public static MvcHtmlString LabelTextFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Label(metadata.Model as string, label));
        }

        public static MvcHtmlString InputNumberFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", int maxLength = short.MaxValue, int minLength = 0, bool required = true, string id = "", string placeholder = "",
            bool disabled = false, bool @readonly = false, string @class = "", bool @exibeTitulo = true)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "number", name, metadata.Model as string, label, @class, id, required, disabled, @readonly, placeholder,
                minLength, maxLength, exibeTitulo));
        }

        public static MvcHtmlString InputEmailFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", int maxLength = 255, int minLength = 1, bool required = true, string id = "", string placeholder = "",
            bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "email", name, metadata.Model as string, label, @class, id, required, disabled, @readonly, placeholder,
                minLength, maxLength));
        }

        public static MvcHtmlString InputUrlFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", int maxLength = 255, int minLength = 1, bool required = true, string id = "", string placeholder = "",
            bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "url", name, metadata.Model as string, label, @class, id, required, disabled, @readonly, placeholder,
                minLength, maxLength));
        }

        public static MvcHtmlString InputCnpjFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", int maxLength = 20, int minLength = 1, bool required = true, string id = "", string placeholder = "",
            bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " cnpj", id, required, disabled, @readonly, placeholder,
                minLength, maxLength));
        }

        public static MvcHtmlString InputPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", int maxLength = 20, int minLength = 3, bool required = true, string id = "", string placeholder = "",
            bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "password", name, metadata.Model as string, label, @class, id, required, disabled, @readonly, placeholder,
                minLength, maxLength));
        }

        public static MvcHtmlString InputPhoneFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " telefone", id, required, disabled, @readonly, placeholder,
                -1, -1));
        }

        public static MvcHtmlString InputDddPhoneFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " ddd_telefone", id, required, disabled, @readonly, placeholder,
                10, -1));
        }

        public static MvcHtmlString InputMoneyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " dinheiro", id, required, disabled, @readonly, placeholder,
                -1, -1));
        }

        public static MvcHtmlString InputPorcentagemFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " porcentagem", id, required, disabled, @readonly, placeholder,
                -1, -1, true));
        }

        public static MvcHtmlString InputCepFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " cep", id, required, disabled, @readonly, placeholder,
                -1, -1));
        }

        public static MvcHtmlString InputDateFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " data", id, required, disabled, @readonly, placeholder,
                -1, -1));
        }

        public static MvcHtmlString InputDateWithMaskFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string label = "", bool required = true, string id = "", string placeholder = "", bool disabled = false, bool @readonly = false, string @class = "")
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            return new MvcHtmlString(Input(helper, "text", name, metadata.Model as string, label, @class + " dataComAutoComplete", id, required, disabled, @readonly, placeholder,
                -1, -1));
        }

        public static MvcHtmlString BackButton(this HtmlHelper helper)
        {
            var html = new StringBuilder("<div>");
            html.Append("<button type='button' class='btn btn-default voltar float-left'> <i class='fa fa-arrow-left'> </i> &nbsp; Voltar</button>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString NewButton(this HtmlHelper helper, string descricaoDoBotao)
        {
            var html = new StringBuilder("<div class='pull-right'>");
            html.Append($"<a href='/{helper.ViewContext.RouteData.Values["controller"]}/Cadastrar' class='btn btn-primary'> <i class='fa fa-plus'> </i> &nbsp; {descricaoDoBotao} </a>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString AddButton(this HtmlHelper helper, string descricaoDoBotao = "Salvar")
        {
            var html = new StringBuilder("<div class='pull-right'>");
            html.Append($"<button type='submit' class='btn btn-primary'> <i class='fa fa-save'> </i> &nbsp; {descricaoDoBotao} </button>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString EditButton(this HtmlHelper helper, string descricaoDoBotao = "Salvar")
        {
            var html = new StringBuilder("<div class='pull-right'>");
            html.Append($"<button type='submit' class='btn btn-primary'> <i class='fa fa-save'> </i> &nbsp; {descricaoDoBotao} </button>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString SearchButton(this HtmlHelper helper, string text = "Pesquisar")
        {
            var html = new StringBuilder();
            html.Append($"<button type='submit' class='btn btn-primary btn-sm'> <i class='fa fa-search'> </i> &nbsp; {text} </button>");

            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        public static IDisposable Box(this HtmlHelper helper, string title, string description = "", string id = "", bool showFooter = false, bool minimize = true)
        {
            var html = new StringBuilder("<div class='ibox float-e-margins'");

            if (!string.IsNullOrEmpty(id))
                html.Append($" id='{id}' >");
            else
                html.Append(" >");

            html.Append("<div class='ibox-title'>");
            html.Append($"<h5>{title}<small>&nbsp; {description}</small></h5>");
            html.Append("<div class='ibox-tools'>");
            html.Append("<a class='collapse-link no-load'><i class='fa fa-chevron-up'></i></a>");
            html.Append("</div></div>");
            html.Append("<div class='ibox-content'>");

            helper.ViewContext.Writer.Write(html.ToString());

            return new BoxBody(helper, showFooter);
        }

        internal class BoxBody : IDisposable
        {
            private readonly HtmlHelper _helper;
            private readonly bool _showFooter;

            public BoxBody(HtmlHelper helper, bool showFooter)
            {
                this._helper = helper;
                this._showFooter = showFooter;
            }

            public void Dispose()
            {
                if (this._showFooter)
                    this._helper.ViewContext.Writer.Write("</div><div class='ibox-footer'> </div></div>");
                else
                    this._helper.ViewContext.Writer.Write("</div></div>");
            }
        }

        private static string Input(HtmlHelper helper, string type, string name, string value, string label, string @class, string id, bool required, bool disabled,
            bool @readonly, string placeholder, int minLength, int maxlength, bool @exibeTitulo = false)
        {
            id = string.IsNullOrEmpty(id) ? name : id;
            label = string.IsNullOrEmpty(label) ? name : label;

            var requiredText = required ? "required" : "";

            var valorRecuperado = helper.ViewData.ModelState[name] != null ? helper.ViewData.ModelState[name].Value.AttemptedValue : "";
            value = string.IsNullOrEmpty(value) ? valorRecuperado : value;

            var html = new StringBuilder(@"<div class='form-group'>");
            if(exibeTitulo) html.Append($@"<label for='{name}' class='control-label'> {label} </label>");
            html.Append($@"<input type='{type}' id='{id}' name='{name}' value=""{value}"" class='form-control {@class}' { requiredText } placeholder='{placeholder}' ");

            if (minLength >= 0)
                html.Append($" minlength={minLength} ");

            if (maxlength >= 0)
                html.Append($" maxlength={maxlength} ");

            if (disabled)
                html.Append(" disabled='disabled' ");

            if (@readonly)
                html.Append(" readonly='readonly' ");

            html.Append(" />");
            html.Append("</div>");

            return html.ToString();
        }

        private static string Input(string type, string name, string value, dynamic properties)
        {
            
            properties.id = string.IsNullOrEmpty(properties.id) ? name : properties.id;
            properties.label = string.IsNullOrEmpty(properties.label) ? name : properties.label;

            var requiredText = properties.required ? "required" : "";

            var html = new StringBuilder("<div class='form-group'>");
            html.Append($"<label for='{name}' class='control-label'> {properties.label} </label>");
            html.Append($"<input type='{type}' id='{properties.id}' name='{name}' value='{value}' class='form-control {properties.@class}' { properties.requiredText } placeholder='{properties.placeholder}' />");
            html.Append("</div>");

            return html.ToString();
        }


        private static string InputSemLabel(string type, string name, string value, dynamic properties)
        {
            properties.id = string.IsNullOrEmpty(properties.id) ? name : properties.id;
            properties.label = string.IsNullOrEmpty(properties.label) ? name : properties.label;

            var html = new StringBuilder("<div class='form-group'>");
            
            html.Append($"<input type='{type}' id='{properties.id}' name='{name}' value='{value}' class='form-control {properties.@class}' { properties.requiredText } placeholder='{properties.placeholder}' />");
            html.Append("</div>");

            return html.ToString();
        }

        private static string Label(string value, string label)
        {
            var html = new StringBuilder("<div class='form-group'>");
            html.Append($"<label for='{label}' class='control-label internal-label'> {label} </label>");
            html.Append($"<p> {value} </p>");
            html.Append("</div>");

            return html.ToString();
        }
    }
}