using System.Web;
using System.Web.Optimization;

namespace OnlineCV
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.UseCdn = true;
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/respond.js"));
            
            // Load Angular and OnlineCV specific scripts
            var onlineCVAngularBundle = new ScriptBundle("~/bundles/onlinecv-angular").Include(
                   "~/Scripts/onlinecv-angular.js"
                  );
            bundles.Add(onlineCVAngularBundle);
            //
            // Load OnlineCV Home controller specific scripts
            var homeBundle = new ScriptBundle("~/bundles/controllers").Include(
                   "~/Scripts/Controllers/admin.js",
                   "~/Scripts/Controllers/home.js",
                   "~/Scripts/Controllers/common.js"
                  );
            bundles.Add(homeBundle);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
