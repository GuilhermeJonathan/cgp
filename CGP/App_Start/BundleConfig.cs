using System.Web;
using System.Web.Optimization;

namespace Cgp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/plugins/bootstrap/js/bootstrap.min.js",
                        "~/plugins/bootstrap/js/bootstrap.bundle.min.js",
                        "~/plugins/bootstrap/js/bootstrap.bundle.min.js.map",
                         "~/Scripts/adminlte.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/adminlte.min.css",
                         "~/Content/Site.css",
                        "~/plugins/fontawesome-free/css/all.min.css"));
        }
    }
}
