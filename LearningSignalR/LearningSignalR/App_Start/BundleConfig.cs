using System.Web.Optimization;

namespace LearningSignalR
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.RegisterScripts();
            bundles.RegisterStyles();

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }

        private static void RegisterScripts(this BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/assets/js/jquery-3.1.0.js",
                "~/assets/js/jquery.validate.js",
                "~/assets/js/bootstrap.js",
                "~/assets/js/respond.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/assets/js/modernizr-2.8.3.js"));
        }

        private static void RegisterStyles(this BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/assets/css/bootstrap.css",
                "~/assets/css/bootstrap-theme.css",
                "~/assets/css/site.css"));
        }
    }
}
