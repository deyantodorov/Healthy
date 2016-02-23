namespace HealthySystem.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);

            // TODO: Set TRUE in production
            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-1.11.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/manager").Include(
                      "~/Scripts/app/manager.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/app/site.js"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/manager").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/dimensions.css",
                     "~/Content/admin.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}