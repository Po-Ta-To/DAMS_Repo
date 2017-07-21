using System.Web;
using System.Web.Optimization;

namespace DAMS_03
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // ** For calendar master
            bundles.Add(new StyleBundle("~/Content/bootstrap/css")
                        .Include("~/Content/zabuto_calendar.css")
                        .Include("~/Content/zabuto_calendar.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                  .Include("~/Scripts/zabuto_calendar.js",
                   "~/Scripts/zabuto_calendar.min.js"));


            // For angular calendar
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap")
            //      .Include("~/Scripts/angular-bootstrap-calendar-tpls.js",
            //      "~/Scripts/angular-bootstrap-calendar-tpls.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap")
            //      .Include("~/Scripts/angular-bootstrap-calendar.js",
            //      "~/Scripts/angular-bootstrap-calendar.mins.js"));
        }
    }
}