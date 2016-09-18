using System;
using System.Collections.Generic;
using System.Globalization;

namespace LearningSignalR.Infrastructure.Html
{
    /// <summary>
    /// https://developer.mozilla.org/en/docs/Web/HTML/Element/Input
    /// </summary>
    public class HtmlInputArgs : BaseHtmlArgs
    {
        public HtmlInputArgs() : base() { }

        public HtmlInputArgs(int? tabIndex, string cssClass = "form-control") : base(tabIndex, cssClass) { }

        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }

        public int? Min { get; set; }
        public int? Max { get; set; }

        public bool? IsReadonly { get; set; }
        public bool? IsHidden { get; set; }

        public string DataType { get; set; }
        public string DataLocale { get; set; }

        public string Placeholder { get; set; }

        public IDictionary<string, object> GetHtmlAttributes(HtmlInputType inputType)
        {
            var htmlAttributes = base.GetHtmlAttributes();

            if (string.IsNullOrEmpty(this.DataType) == false)
                htmlAttributes.Add("data-type", this.DataType);

            if (string.IsNullOrEmpty(this.Style) == false)
                htmlAttributes.Add("data-locale", this.DataLocale);

            if (inputType == HtmlInputType.Text ||
                inputType == HtmlInputType.Email ||
                inputType == HtmlInputType.Search ||
                inputType == HtmlInputType.Password ||
                inputType == HtmlInputType.Tel ||
                inputType == HtmlInputType.Url)
            {
                if (this.MinLength.HasValue)
                    htmlAttributes.Add("minlength", this.MinLength.Value.ToString(NumberFormatInfo.InvariantInfo));

                if (this.MaxLength.HasValue)
                    htmlAttributes.Add("maxlength", this.MaxLength.Value.ToString(NumberFormatInfo.InvariantInfo));
            }

            if (inputType == HtmlInputType.Number)
            {
                if (this.Min.HasValue)
                    htmlAttributes.Add("min", this.Min.Value.ToString(NumberFormatInfo.InvariantInfo));

                if (this.Max.HasValue)
                    htmlAttributes.Add("max", this.Max.Value.ToString(NumberFormatInfo.InvariantInfo));
            }

            if (this.IsReadonly.HasValue && this.IsReadonly.Value)
                htmlAttributes.Add("readonly", "true");

            if (this.IsHidden.HasValue && this.IsHidden.Value)
                inputType = HtmlInputType.Hidden;

            switch (inputType)
            {
                case HtmlInputType.CheckBox:
                    htmlAttributes.Add("type", "checkbox");
                    break;
                case HtmlInputType.Number:
                    htmlAttributes.Add("type", "number");
                    break;
                case HtmlInputType.Text:
                    htmlAttributes.Add("type", "text");
                    break;
                case HtmlInputType.Email:
                    htmlAttributes.Add("type", "email");
                    break;
                case HtmlInputType.Search:
                    htmlAttributes.Add("type", "search");
                    break;
                case HtmlInputType.Password:
                    htmlAttributes.Add("type", "password");
                    break;
                case HtmlInputType.Tel:
                    htmlAttributes.Add("type", "tel");
                    break;
                case HtmlInputType.Url:
                    htmlAttributes.Add("type", "url");
                    break;
                case HtmlInputType.Hidden:
                    htmlAttributes.Add("type", "hidden");
                    break;
                default:
                    throw new ArgumentException("Unknown enum type: " + inputType);
            }

            if (string.IsNullOrEmpty(this.Placeholder) == false)
                htmlAttributes.Add("placeholder ", this.Placeholder);

            return htmlAttributes;
        }
    }
}