using System.Collections.Generic;
using System.Globalization;

namespace LearningSignalR.Infrastructure.Html
{
    public abstract class BaseHtmlArgs
    {
        protected BaseHtmlArgs() : this(null)
        {
        }

        protected BaseHtmlArgs(int? tabIndex, string cssClass = "form-control")
        {
            this.TabIndex = tabIndex;
            this.CssClass = cssClass;
        }

        public int? TabIndex { get; set; }
        public string CssClass { get; set; }
        public string Style { get; set; }
        public string ShowIf { get; set; }

        protected IDictionary<string, object> GetHtmlAttributes()
        {
            var htmlAttributes = new Dictionary<string, object>();

            if (this.TabIndex.HasValue)
                htmlAttributes.Add("tabindex", this.TabIndex.Value.ToString(NumberFormatInfo.InvariantInfo));

            if (string.IsNullOrEmpty(this.CssClass) == false)
                htmlAttributes.Add("class", this.CssClass.Replace(",", " "));

            if (string.IsNullOrEmpty(this.Style) == false)
                htmlAttributes.Add("style", this.Style);

            if (string.IsNullOrEmpty(this.ShowIf) == false)
                htmlAttributes.Add("data-show-if", this.ShowIf);

            return htmlAttributes;
        }
    }

}