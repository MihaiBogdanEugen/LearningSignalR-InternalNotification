using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using LearningSignalR.BackEnd;
using LearningSignalR.Db;

namespace LearningSignalR.Infrastructure.Html
{
    public static class HtmlHelperExtensions
    {
        public static string IsActive(this HtmlHelper htmlHelper, string controller = null, string action = null)
        {
            const string activeClass = "active";

            var actualAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            var actualController = (string)htmlHelper.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = actualController;

            if (string.IsNullOrEmpty(action))
                action = actualAction;

            return (controller == actualController && action == actualAction) ? activeClass : string.Empty;
        }

        #region Label and Editors (one next to the other)

        public static MvcHtmlString LabelCheckBoxBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.Style = "width: 30px";
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.CheckBox);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.CheckBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelPasswordTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Password);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.PasswordFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelDateTimeTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.DataType = "date-time";
            args.DataLocale = "ro";
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelDateTimeTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.DataType = "date-time";
            args.DataLocale = "ro";
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelDateTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.DataType = "date";
            args.DataLocale = "ro";
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelDateTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.DataType = "date";
            args.DataLocale = "ro";
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelTextAreaFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.BigStringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            innerDiv.InnerHtml += htmlHelper.TextAreaFor(expression, 10, 30, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelSmallTextAreaFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("col-md-2");
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.BigStringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextAreaFor(expression, 5, 30, htmlAttributes);
            innerDiv.InnerHtml += " ";
            innerDiv.InnerHtml += htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelNumberTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, HtmlInputArgs args = null)
            where TProperty : struct, IComparable, IComparable<TProperty>, IConvertible, IEquatable<TProperty>, IFormattable
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Number);

            if ((args.IsHidden.HasValue && args.IsHidden.Value) == false)
            {
                var label = new TagBuilder("label");
                label.Attributes.Add("for", forId);
                label.AddCssClass("col-md-2");
                label.AddCssClass("control-label");
                label.SetInnerText(labelText);
                div.InnerHtml += label.ToString(TagRenderMode.Normal);
            }

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass((args.IsHidden.HasValue && args.IsHidden.Value) ? "col-md-offset-2 col-md-10" : "col-md-10");
            innerDiv.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, int>> expression, IEnumerable<SelectListItem> selectList, HtmlSelectArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.SetInnerText(labelText);

            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlSelectArgs();
            var htmlAttributes = args.GetHtmlAttributes();

            innerDiv.InnerHtml += htmlHelper.DropDownListFor(expression, selectList, "Alegeti o optiune", htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, int?>> expression, IEnumerable<SelectListItem> selectList, HtmlSelectArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.SetInnerText(labelText);

            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlSelectArgs();
            var htmlAttributes = args.GetHtmlAttributes();

            innerDiv.InnerHtml += htmlHelper.DropDownListFor(expression, selectList, "Alegeti o optiune", htmlAttributes);
            innerDiv.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        #endregion Label and Editors (one next to the other)

        #region Label And Editors (one under the other)

        public static MvcHtmlString DtDdLabelAboveNumberTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, HtmlInputArgs args = null)
            where TProperty : struct, IComparable, IComparable<TProperty>, IConvertible, IEquatable<TProperty>, IFormattable
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Number);

            var dt = new TagBuilder("dt");
            var dd = new TagBuilder("dd");

            if ((args.IsHidden.HasValue && args.IsHidden.Value) == false)
            {
                var label = new TagBuilder("label");
                label.Attributes.Add("for", forId);
                label.AddCssClass("col-md-2");
                label.AddCssClass("control-label");
                label.SetInnerText(labelText);
                dt.InnerHtml += label.ToString(TagRenderMode.Normal);
            }

            dd.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            dd.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdLabelAboveSmallTextAreaFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            var dt = new TagBuilder("dt");
            dt.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.BigStringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.TextAreaFor(expression, 5, 30, htmlAttributes);
            dd.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdLabelAboveTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            var dt = new TagBuilder("dt");
            dt.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            dd.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdLabelAboveDateTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            var dt = new TagBuilder("dt");
            dt.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            args.DataType = "date";
            args.DataLocale = "ro";
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            dd.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdLabelAboveDateTimeTextBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression, HtmlInputArgs args = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.SetInnerText(labelText);
            var dt = new TagBuilder("dt");
            dt.InnerHtml += label.ToString(TagRenderMode.Normal);

            if (args == null)
                args = new HtmlInputArgs();
            args.MaxLength = DbConstants.StringLength;
            args.DataType = "date-time";
            args.DataLocale = "ro";
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.TextBoxFor(expression, htmlAttributes);
            dd.InnerHtml += " " + htmlHelper.ValidationMessageFor(expression, "", new Dictionary<string, object> { { "class", "text-danger" } });

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        #endregion Label And Editors (one under the other)

        #region Table

        public static MvcHtmlString TableHeader<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool withSorting = true)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression).Replace("Header.", string.Empty);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (withSorting == false)
            {
                var span = new TagBuilder("span");
                span.SetInnerText(labelText);
                return MvcHtmlString.Create("\r\n" + span.ToString(TagRenderMode.Normal));
            }

            var actionName = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var controllerName = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            var sortOrder = htmlHelper.ViewData[htmlFieldName + "SortParam"];
            var cssClass = htmlHelper.ViewData[htmlFieldName + "SortClass"];

            var routeValues = new RouteValueDictionary { { "sortOrder", sortOrder } };
            var queryString = htmlHelper.ViewContext.HttpContext.Request.QueryString;
            foreach (var keyAsString in queryString.Keys.Cast<object>()
                .Select(key => key.ToString())
                .Where(keyAsString => routeValues.ContainsKey(keyAsString) == false))
                routeValues.Add(keyAsString, queryString[keyAsString]);

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = string.IsNullOrEmpty(controllerName)
                ? urlHelper.Action(actionName, routeValues)
                : urlHelper.Action(actionName, controllerName, routeValues);
            urlHelper = null;

            var a = new TagBuilder("a");
            if (cssClass != null && string.IsNullOrEmpty(cssClass.ToString()) == false)
                a.AddCssClass(cssClass.ToString());
            a.Attributes.Add("href", url);
            a.SetInnerText(labelText);

            return MvcHtmlString.Create("\r\n" + a.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString TableHeader<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (isAdministrator || onlyForAdministratives)
            {
                var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                var htmlFieldName = ExpressionHelper.GetExpressionText(expression).Replace("Header.", string.Empty);
                var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
                var actionName = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
                var controllerName = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
                var sortOrder = htmlHelper.ViewData[htmlFieldName + "SortParam"];
                var cssClass = htmlHelper.ViewData[htmlFieldName + "SortClass"];

                var routeValues = new RouteValueDictionary { { "sortOrder", sortOrder } };
                var queryString = htmlHelper.ViewContext.HttpContext.Request.QueryString;
                foreach (var keyAsString in queryString.Keys.Cast<object>()
                    .Select(key => key.ToString())
                    .Where(keyAsString => routeValues.ContainsKey(keyAsString) == false))
                    routeValues.Add(keyAsString, queryString[keyAsString]);

                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                var url = string.IsNullOrEmpty(controllerName)
                    ? urlHelper.Action(actionName, routeValues)
                    : urlHelper.Action(actionName, controllerName, routeValues);
                urlHelper = null;

                var a = new TagBuilder("a");
                if (cssClass != null && string.IsNullOrEmpty(cssClass.ToString()) == false)
                    a.AddCssClass(cssClass.ToString());
                a.Attributes.Add("href", url);
                a.SetInnerText(labelText);

                return MvcHtmlString.Create("\r\n" + a.ToString(TagRenderMode.Normal));
            }

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString TablePager<TModel>(this HtmlHelper<TModel> htmlHelper, int currentPageNo, int totalNoOfPages)
        {
            if (totalNoOfPages <= 1)
                return MvcHtmlString.Empty;

            var previousPageNo = currentPageNo - 1;
            if (previousPageNo < 0)
                previousPageNo = 0;

            var nextPageNo = currentPageNo + 1;
            if (nextPageNo >= totalNoOfPages)
                nextPageNo = currentPageNo;

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination pagination-md");

            ul.InnerHtml += " " + HtmlHelperExtensions.GetNavLi(htmlHelper.GetUrl(previousPageNo), "Inapoi", "«", false, currentPageNo == 0);
            foreach (var pageNo in HtmlHelperExtensions.GetShownPages(currentPageNo, totalNoOfPages))
            {
                if (pageNo.HasValue)
                    ul.InnerHtml += " " + HtmlHelperExtensions.GetNavLi(
                                        url: htmlHelper.GetUrl(pageNo.Value),
                                        areaLabel: (pageNo.Value + 1).ToString(),
                                        spanText: (pageNo.Value + 1).ToString(),
                                        currentPage: pageNo.Value == currentPageNo,
                                        disable: pageNo.Value == currentPageNo);
                else
                    ul.InnerHtml += " " + HtmlHelperExtensions.GetNavLi("#", "...", "...");
            }
            ul.InnerHtml += " " + HtmlHelperExtensions.GetNavLi(htmlHelper.GetUrl(nextPageNo), "Inainte", "»", false, currentPageNo == totalNoOfPages - 1);
            return MvcHtmlString.Create("\r\n" + ul.ToString(TagRenderMode.Normal));
        }

        private static string GetUrl(this HtmlHelper htmlHelper, int currentPageNo)
        {
            currentPageNo += 1;

            var actionName = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
            var controllerName = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();

            var routeValues = new RouteValueDictionary { { "pageNo", currentPageNo } };
            var queryString = htmlHelper.ViewContext.HttpContext.Request.QueryString;
            foreach (var keyAsString in queryString.Keys.Cast<object>()
                .Select(key => key.ToString())
                .Where(keyAsString => routeValues.ContainsKey(keyAsString) == false))
                routeValues.Add(keyAsString, queryString[keyAsString]);

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = string.IsNullOrEmpty(controllerName)
                ? urlHelper.Action(actionName, routeValues)
                : urlHelper.Action(actionName, controllerName, routeValues);
            urlHelper = null;

            return url;
        }

        private static MvcHtmlString GetNavLi(string url, string areaLabel, string spanText, bool currentPage = false, bool disable = false)
        {
            var li = new TagBuilder("li");
            if (currentPage)
                li.AddCssClass("active");

            if (disable)
                li.AddCssClass("not-active");

            var a = new TagBuilder("a");
            a.Attributes.Add("href", url);
            a.Attributes.Add("area-label", areaLabel);

            var span = new TagBuilder("span");
            span.Attributes.Add("aria-hidden", "true");
            span.SetInnerText(spanText);

            a.InnerHtml += " " + span.ToString(TagRenderMode.Normal);
            li.InnerHtml += " " + a.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + li.ToString(TagRenderMode.Normal));
        }

        private static IEnumerable<int?> GetShownPages(int currentPageNo, int totalNoOfPages)
        {
            var result = new List<int>();

            var temp = currentPageNo - 3;
            if (temp >= 0 && temp < totalNoOfPages)
                result.Add(temp);

            temp = currentPageNo - 2;
            if (temp >= 0 && temp < totalNoOfPages)
                result.Add(temp);

            temp = currentPageNo - 1;
            if (temp >= 0 && temp < totalNoOfPages)
                result.Add(temp);

            result.Add(currentPageNo);

            temp = currentPageNo + 1;
            if (temp >= 0 && temp < totalNoOfPages)
                result.Add(temp);

            temp = currentPageNo + 2;
            if (temp >= 0 && temp < totalNoOfPages)
                result.Add(temp);

            temp = currentPageNo + 3;
            if (temp >= 0 && temp < totalNoOfPages)
                result.Add(temp);

            if (result[0] != 0)
                result.Insert(0, 0);

            if (result[result.Count - 1] != totalNoOfPages - 1)
                result.Add(totalNoOfPages - 1);

            var newResult = new List<int?> { result[0] };

            for (var index = 1; index < result.Count; index++)
            {
                var latest = newResult[newResult.Count - 1];
                var current = result[index];
                if (latest.HasValue && current != latest + 1)
                    newResult.Add(null);

                newResult.Add(current);
            }

            return newResult;
        }

        #endregion Table

        #region Buttons

        public static MvcHtmlString ButtonsFormToogle<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            Expression<Func<TModel, int>> expressionHidden, string textWhenTrue, string textWhenFalse,
            string cssClassWhenTrue, string cssClassWhenFalse,
            string actionName, string controllerName, object routeValues = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (bool)metadata.Model;

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = routeValues == null
                ? urlHelper.Action(actionName, controllerName)
                : urlHelper.Action(actionName, controllerName, routeValues);
            urlHelper = null;

            var form = new TagBuilder("form");
            form.Attributes.Add("action", url);
            form.Attributes.Add("method", "post");

            var button = new TagBuilder("input");
            button.Attributes.Add("class", value ? cssClassWhenTrue : cssClassWhenFalse);
            button.Attributes.Add("type", "submit");
            button.Attributes.Add("value", value ? textWhenTrue : textWhenFalse);

            form.InnerHtml += " " + htmlHelper.AntiForgeryToken();
            form.InnerHtml += " " + htmlHelper.HiddenFor(expressionHidden);
            form.InnerHtml += " " + button.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + form.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ButtonsDivSave(this HtmlHelper htmlHelper, string saveButtonText = "Save")
        {
            var outterDiv = new TagBuilder("div");
            outterDiv.AddCssClass("form-group");

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-offset-2 col-md-10");

            var button = new TagBuilder("button");
            button.AddCssClass("btn btn-md btn-primary");
            button.Attributes.Add("type", "submit");
            button.SetInnerText(saveButtonText);

            innerDiv.InnerHtml += " " + button.ToString(TagRenderMode.Normal);
            outterDiv.InnerHtml += " " + innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + outterDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ButtonsDivFilterReset(this HtmlHelper htmlHelper, string saveButtonText = "Filter",
            string cancelButtonText = "Reset", string cancelActionName = "List", string cancelControllerName = null)
        {
            return htmlHelper.ButtonsDivSaveCancel(saveButtonText, cancelButtonText, cancelActionName, cancelControllerName);
        }

        public static MvcHtmlString ButtonsDivSaveCancel(this HtmlHelper htmlHelper, string saveButtonText = "Save",
            string cancelButtonText = "Cancel", string cancelActionName = "List", string cancelControllerName = null)
        {
            var outterDiv = new TagBuilder("div");
            outterDiv.AddCssClass("form-group");

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-offset-2 col-md-10");

            var button = new TagBuilder("button");
            button.AddCssClass("btn btn-md btn-primary");
            button.Attributes.Add("type", "submit");
            button.SetInnerText(saveButtonText);

            var htmlAttributes = new
            {
                @class = "btn btn-md btn-default"
            };

            if (string.IsNullOrEmpty(cancelControllerName))
                innerDiv.InnerHtml += " " + htmlHelper.ActionLink(cancelButtonText, cancelActionName, null, htmlAttributes);
            else
                innerDiv.InnerHtml += " " + htmlHelper.ActionLink(cancelButtonText, cancelActionName, cancelControllerName, null, htmlAttributes);
            innerDiv.InnerHtml += " " + button.ToString(TagRenderMode.Normal);
            outterDiv.InnerHtml += " " + innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + outterDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ButtonNew(this HtmlHelper htmlHelper, string controllerName = null, string newButtonText = "New", string newActionName = "New", object routeValues = null)
        {
            return string.IsNullOrEmpty(controllerName)
                ? htmlHelper.ActionLink(newButtonText, newActionName, routeValues, new { @class = "btn btn-info btn-md" })
                : htmlHelper.ActionLink(newButtonText, newActionName, controllerName, routeValues, new
                {
                    @class = "btn btn-info btn-md"
                });
        }

        #endregion Buttons

        #region Filter

        public static MvcHtmlString FilterTextBox(this HtmlHelper htmlHelper, string name,
            string labelText = null, HtmlInputArgs args = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(labelText))
                labelText = name.Substring(0, 1).ToUpperInvariant() + name.Substring(1).ToLowerInvariant();

            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.SetInnerText(labelText);

            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlInputArgs();
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);
            innerDiv.InnerHtml += htmlHelper.TextBox(name, null, htmlAttributes);
            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FilterDateTextBox(this HtmlHelper htmlHelper, string name,
            string labelText = null, HtmlInputArgs args = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(labelText))
                labelText = name.Substring(0, 1).ToUpperInvariant() + name.Substring(1).ToLowerInvariant();

            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.SetInnerText(labelText);

            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlInputArgs();
            args.DataType = "date";
            args.DataLocale = "ro";
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);
            innerDiv.InnerHtml += htmlHelper.TextBox(name, null, htmlAttributes);
            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FilterDateTimeTextBox(this HtmlHelper htmlHelper, string name,
            string labelText = null, HtmlInputArgs args = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(labelText))
                labelText = name.Substring(0, 1).ToUpperInvariant() + name.Substring(1).ToLowerInvariant();

            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.SetInnerText(labelText);

            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlInputArgs();
            args.DataType = "date-time";
            args.DataLocale = "ro";
            var htmlAttributes = args.GetHtmlAttributes(HtmlInputType.Text);
            innerDiv.InnerHtml += htmlHelper.TextBox(name, null, htmlAttributes);
            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FilterDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<SelectListItem> selectList, string labelText = null, HtmlSelectArgs args = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(labelText))
                labelText = name.Substring(0, 1).ToUpperInvariant() + name.Substring(1).ToLowerInvariant();

            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var label = new TagBuilder("label");
            label.Attributes.Add("for", forId);
            label.AddCssClass("control-label");
            label.AddCssClass("col-md-2");
            label.SetInnerText(labelText);

            div.InnerHtml += label.ToString(TagRenderMode.Normal);

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-10");

            if (args == null)
                args = new HtmlSelectArgs();
            var htmlAttributes = args.GetHtmlAttributes();
            innerDiv.InnerHtml += htmlHelper.DropDownList(name, selectList, "Choose an option", htmlAttributes);

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        #endregion Filter

        #region Forms

        public static MvcForm DefaultPostForm(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return htmlHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, HtmlHelper.AnonymousObjectToHtmlAttributes(new
            {
                @class = "form-horizontal",
                role = "form"
            }));
        }

        public static MvcForm DefaultGetForm(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return htmlHelper.BeginForm(actionName, controllerName, new RouteValueDictionary(), FormMethod.Get, HtmlHelper.AnonymousObjectToHtmlAttributes(new
            {
                @class = "form-horizontal",
                role = "form"
            }));
        }

        #endregion Forms

        #region DtDd DetailsFor

        public static MvcHtmlString DtDdDetailsFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.DisplayFor(expression);

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DtDdBoolDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, string valueForTrue, string valueForFalse)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var value = (bool)metadata.Model;

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(value ? valueForTrue : valueForFalse);

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdIsCapitalDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            string valueForTrue = "District capital", string valueForFalse = "Not District capital")
        {
            return htmlHelper.DtDdBoolDetailsFor(expression, valueForTrue, valueForFalse);
        }

        public static MvcHtmlString DtDdHasSupportDaylightSavingTimeDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            string valueForTrue = "Supports DST", string valueForFalse = "Does not support DST")
        {
            return htmlHelper.DtDdBoolDetailsFor(expression, valueForTrue, valueForFalse);
        }

        public static MvcHtmlString DtDdPriceDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, decimal>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(((decimal)metadata.Model).AsRoPriceText());

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdPriceDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, decimal?>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var value = (decimal?)metadata.Model;
            if (value.HasValue == false)
                return MvcHtmlString.Empty;

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(((decimal)metadata.Model).AsRoPriceText());

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DtDdDistanceDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, decimal>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(((decimal)metadata.Model).AsKmText());

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdDistanceDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, decimal?>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var value = (decimal?)metadata.Model;
            if (value.HasValue == false)
                return MvcHtmlString.Empty;

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(((decimal)metadata.Model).AsKmText());

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DtDdDateTimeDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var value = (DateTime)metadata.Model;
            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(value.AsRoTimeZoneText());

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DtDdDateTimeDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression, string dateTimeFormat = Constants.DateTimeFormat)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var value = (DateTime?)metadata.Model;
            if (value.HasValue == false)
                return MvcHtmlString.Empty;

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.Encode(value.Value.ToString(dateTimeFormat, DateTimeFormatInfo.InvariantInfo));

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DtDdDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var dd = new TagBuilder("dd");
            dd.InnerHtml += htmlHelper.DisplayFor(expression);

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dd.ToString(TagRenderMode.Normal) + "\r\n" + dt.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DtDdUrlActionDetailsFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = string.IsNullOrEmpty(controllerName)
                ? urlHelper.Action(actionName, routeValues)
                : urlHelper.Action(actionName, controllerName, routeValues);
            urlHelper = null;

            var a = new TagBuilder("a");
            a.Attributes.Add("href", url);
            a.InnerHtml += " " + htmlHelper.Encode(metadata.Model);

            var dd = new TagBuilder("dd");
            dd.InnerHtml += a.ToString(TagRenderMode.Normal);

            var dt = new TagBuilder("dt");
            dt.InnerHtml += htmlHelper.Encode(labelText);

            return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
        }

        #endregion DtDd DetailsFor

        #region DtDd Audit DetailsFor

        public static MvcHtmlString DtDdAddedAtDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression, IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (isAdministrator || onlyForAdministratives)
                return htmlHelper.DtDdDateTimeDetailsFor(expression);

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString DtDdModifiedAtDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression, IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (isAdministrator || onlyForAdministratives)
                return htmlHelper.DtDdDateTimeDetailsFor(expression);

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString DtDdIsDeletedDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (isAdministrator || onlyForAdministratives)
            {
                var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
                var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

                var value = (bool)metadata.Model;
                var dd = new TagBuilder("dd");
                dd.InnerHtml += htmlHelper.Encode(value ? "Ascuns" : "Vizibil");

                var dt = new TagBuilder("dt");
                dt.InnerHtml += htmlHelper.Encode(labelText);

                return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
            }

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString DtDdAddedByDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression,
            string actionName, string controllerName, RouteValueDictionary routeValues, IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (isAdministrator || onlyForAdministratives)
            {
                var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
                var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

                var value = (string)metadata.Model;

                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                var url = string.IsNullOrEmpty(controllerName)
                    ? urlHelper.Action(actionName, routeValues)
                    : urlHelper.Action(actionName, controllerName, routeValues);
                urlHelper = null;

                var a = new TagBuilder("a");
                a.Attributes.Add("href", url);
                a.InnerHtml += " " + htmlHelper.Encode(value);

                var dd = new TagBuilder("dd");
                dd.InnerHtml += a.ToString(TagRenderMode.Normal);

                var dt = new TagBuilder("dt");
                dt.InnerHtml += htmlHelper.Encode(labelText);

                return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
            }

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString DtDdModifiedByDetailsFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression,
            string actionName, string controllerName, RouteValueDictionary routeValues, IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (isAdministrator || onlyForAdministratives)
            {
                var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
                var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

                var value = (string)metadata.Model;
                if (string.IsNullOrEmpty(value))
                    return MvcHtmlString.Empty;

                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                var url = string.IsNullOrEmpty(controllerName)
                    ? urlHelper.Action(actionName, routeValues)
                    : urlHelper.Action(actionName, controllerName, routeValues);
                urlHelper = null;

                var a = new TagBuilder("a");
                a.Attributes.Add("href", url);
                a.InnerHtml += " " + htmlHelper.Encode(value);

                var dd = new TagBuilder("dd");
                dd.InnerHtml += a.ToString(TagRenderMode.Normal);

                var dt = new TagBuilder("dt");
                dt.InnerHtml += htmlHelper.Encode(labelText);

                return MvcHtmlString.Create("\r\n" + dt.ToString(TagRenderMode.Normal) + "\r\n" + dd.ToString(TagRenderMode.Normal));
            }

            return MvcHtmlString.Empty;
        }

        #endregion DtDd Audit DetailsFor

        #region DisplayFor

        public static MvcHtmlString DateTimeDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (DateTime)metadata.Model;

            var span = new TagBuilder("span");
            span.SetInnerText(value.AsRoTimeZoneText());

            return MvcHtmlString.Create("\r\n" + span.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DateTimeDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime?>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (DateTime?)metadata.Model;
            if (value.HasValue == false)
                return MvcHtmlString.Empty;

            var span = new TagBuilder("span");
            span.SetInnerText(value.Value.AsRoTimeZoneText());

            return MvcHtmlString.Create("\r\n" + span.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString BoolDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            string valueForTrue, string valueForFalse, string cssClassForTrue = null, string cssClassForFalse = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = (bool)metadata.Model;

            var span = new TagBuilder("span");
            span.AddCssClass("label");
            if (value)
            {
                if (string.IsNullOrEmpty(cssClassForTrue) == false)
                    span.AddCssClass(cssClassForTrue);
            }
            else
            {
                if (string.IsNullOrEmpty(cssClassForFalse) == false)
                    span.AddCssClass(cssClassForFalse);
            }
            span.SetInnerText(value ? valueForTrue : valueForFalse);

            return MvcHtmlString.Create("\r\n" + span.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString IsApprovedDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            string valueForTrue = "Aprobat", string valueForFalse = "Dezaprobat")
        {
            return htmlHelper.BoolDisplayFor(expression, valueForTrue, valueForFalse, "label-primary", "label-danger");
        }

        public static MvcHtmlString IsSelectedDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            string valueForTrue = "Selectat", string valueForFalse = "Deselectat")
        {
            return htmlHelper.BoolDisplayFor(expression, valueForTrue, valueForFalse, "label-primary", "label-danger");
        }

        public static MvcHtmlString IsDeletedDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            IPrincipal principal, bool onlyForAdministratives = true)
        {
            var isAdministrator = principal.IsInRole(DbConstants.Roles.Administrator.Name) || principal.IsInRole(DbConstants.Roles.PowerUser.Name);

            if (!isAdministrator && !onlyForAdministratives)
                return MvcHtmlString.Empty;

            return htmlHelper.BoolDisplayFor(expression, "Ascuns", "Vizibil", "label-warning", "label-success");
        }

        #endregion DisplayFor

        public static MvcHtmlString CheckBoxesGroup<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IList<SelectListItem>>> expression, string groupId)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            var forId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            var items = (IList<SelectListItem>)metadata.Model;

            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var groupLabel = new TagBuilder("label");
            groupLabel.Attributes.Add("for", forId);
            groupLabel.AddCssClass("col-md-2");
            groupLabel.AddCssClass("control-label");
            groupLabel.SetInnerText(labelText);
            div.InnerHtml += groupLabel.ToString(TagRenderMode.Normal);

            foreach (var item in items)
            {
                var input = new TagBuilder("input");
                input.Attributes.Add("type", "checkbox");
                input.Attributes.Add("name", htmlFieldName);
                input.Attributes.Add("value", item.Value);
                if (item.Selected)
                    input.Attributes.Add("checked", "");

                var span = new TagBuilder("span");
                span.AddCssClass("fa fa-check");

                var label = new TagBuilder("label");
                label.InnerHtml += " " + input.ToString(TagRenderMode.StartTag);
                label.InnerHtml += " " + span.ToString(TagRenderMode.Normal);
                label.InnerHtml += " " + htmlHelper.Encode(item.Text);

                var divCheckBox = new TagBuilder("div");
                divCheckBox.AddCssClass("checkbox c-checkbox");
                divCheckBox.InnerHtml += " " + label.ToString(TagRenderMode.Normal);

                div.InnerHtml += " " + divCheckBox.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {
            var prefix = ExpressionHelper.GetExpressionText(expression);
            var model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;

            if (!string.IsNullOrEmpty(helper.ViewData.TemplateInfo.HtmlFieldPrefix))
                prefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix + "." + prefix;

            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo
                {
                    HtmlFieldPrefix = prefix
                },
                ModelMetadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData)
            };

            return helper.Partial(partialViewName, model, viewData);
        }

        public static MvcHtmlString DefaultValidationSummary(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ValidationSummary(false, "", new Dictionary<string, object> { { "class", "text-danger" } }, (string)null);
        }

        public static MvcHtmlString ReCaptcha<TModel>(this HtmlHelper<TModel> htmlHelper, string siteKey, int? tabIndex = null)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("col-md-offset-2 col-md-10");

            var reCaptcha = new TagBuilder("div");
            reCaptcha.AddCssClass("g-recaptcha");
            reCaptcha.Attributes.Add("data-sitekey", siteKey);
            if (tabIndex.HasValue)
                reCaptcha.Attributes.Add("tabindex", tabIndex.Value.ToString());

            innerDiv.InnerHtml += reCaptcha.ToString(TagRenderMode.EndTag);

            div.InnerHtml += innerDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create("\r\n" + div.ToString(TagRenderMode.Normal));
        }

        //        public static MvcHtmlString ReadMore(this HtmlHelper htmlHelper, string text, string code, string actionName, 
        //            string controllerName = null, string readMoreText = "Citeste mai multe", int noOfWords = 20)
        //        {
        //            if (text == null)
        //                throw new ArgumentNullException(nameof(text));
        //
        //            if (code == null)
        //                throw new ArgumentNullException(nameof(code));
        //
        //            if (string.IsNullOrEmpty(actionName))
        //                throw new ArgumentNullException(nameof(actionName));
        //
        //            var routeValues = new RouteValueDictionary { { "code", code } };
        //            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
        //            var url = string.IsNullOrEmpty(controllerName)
        //                ? urlHelper.Action(actionName, routeValues)
        //                : urlHelper.Action(actionName, controllerName, routeValues);
        //            urlHelper = null;
        //
        //            var a = new TagBuilder("a");
        //            a.Attributes.Add("href", url);
        //            a.SetInnerText(readMoreText);
        //
        //            var span = new TagBuilder("span");
        //            span.SetInnerText(htmlHelper.Encode(text.GetNoOfWords(noOfWords)));
        //            span.InnerHtml += " " + a.ToString(TagRenderMode.Normal);
        //            
        //            return MvcHtmlString.Create("\r\n" + span.ToString(TagRenderMode.Normal));
        //        }
    }
}