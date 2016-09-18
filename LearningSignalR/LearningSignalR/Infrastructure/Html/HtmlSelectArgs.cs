using System.Collections.Generic;

namespace LearningSignalR.Infrastructure.Html
{
    /// <summary>
    /// https://developer.mozilla.org/en/docs/Web/HTML/Element/Select
    /// </summary>
    public class HtmlSelectArgs : BaseHtmlArgs
    {
        public HtmlSelectArgs() : base()
        {
            this.HasGroupChildren = false;
        }

        public HtmlSelectArgs(int? tabIndex, string cssClass = "form-control") : base(tabIndex, cssClass)
        {
            this.HasGroupChildren = false;
        }

        public bool? AllowsMultipleSelection { get; set; }
        public string DependsUpon { get; set; }
        public string UpdateSource { get; set; }

        public bool HasGroupChildren { get; set; }
        public string UrlForGettingChildren { get; set; }
        public string ChildrenIdsPrefix { get; set; }

        public new IDictionary<string, object> GetHtmlAttributes()
        {
            var htmlAttributes = base.GetHtmlAttributes();

            if (this.AllowsMultipleSelection.HasValue && this.AllowsMultipleSelection.Value)
                htmlAttributes.Add("multiple", "true");

            if (string.IsNullOrEmpty(this.DependsUpon) == false)
                htmlAttributes.Add("data-depends-on", this.DependsUpon);

            if (string.IsNullOrEmpty(this.UpdateSource) == false)
                htmlAttributes.Add("data-update-url", this.UpdateSource);

            if (this.HasGroupChildren)
            {

            }

            return htmlAttributes;
        }
    }
}