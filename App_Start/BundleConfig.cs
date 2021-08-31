using System.Web;
using System.Web.Optimization;

namespace Xyngr
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/AdminCss").Include(
                      "~/Content/app-assets/css/vendors.css"
                      ,"~/Content/app-assets/css/app.css"
                      , "~/Content/app-assets/css/core/menu/menu-types/vertical-menu-modern.css"
                      , "~/Content/app-assets/css/core/colors/palette-gradient.css"
                      , "~/Content/app-assets/vendors/css/charts/jquery-jvectormap-2.0.3.css"
                      , "~/Content/app-assets/vendors/css/charts/morris.css"
                      , "~/Content/app-assets/fonts/simple-line-icons/style.css"
                       , "~/Content/app-assets/css/core/colors/palette-gradient.css"
                      , "~/Content/assets/css/style.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/GridCSS").Include(
                      "~/Content/app-assets/vendors/css/tables/datatable/dataTables.bootstrap4.min.css"
                      , "~/Content/app-assets/vendors/css/tables/extensions/rowReorder.dataTables.min.css"
                      , "~/Content/app-assets/vendors/css/tables/extensions/responsive.dataTables.min.css"
                      , "~/Content/app-assets/vendors/css/forms/icheck/icheck.css"
                      , "~/Content/app-assets/vendors/css/forms/icheck/custom.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/AdminJS").Include(
                      "~/Content/app-assets/vendors/js/vendors.min.js"
                      , "~/Content/app-assets/vendors/js/charts/chart.min.js"
                      , "~/Content/app-assets/vendors/js/charts/raphael-min.js"
                      , "~/Content/app-assets/vendors/js/charts/morris.min.js"
                      , "~/Content/app-assets/vendors/js/charts/jvector/jquery-jvectormap-2.0.3.min.js"
                      , "~/Content/app-assets/vendors/js/charts/jvector/jquery-jvectormap-world-mill.js"
                      , "~/Content/app-assets/data/jvector/visitor-data.js"
                      , "~/Content/app-assets/js/core/app-menu.js"
                      , "~/Content/app-assets/js/core/app.js"
                      , "~/Content/app-assets/js/scripts/customizer.js"
                      , "~/Content/app-assets/js/scripts/extensions/block-ui.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/GridJS").Include(
                      "~/Content/app-assets/vendors/js/tables/jquery.dataTables.min.js"
                      , "~/Content/app-assets/vendors/js/tables/datatable/dataTables.bootstrap4.min.js"
                      , "~/Content/app-assets/vendors/js/tables/datatable/dataTables.responsive.min.js"
                      , "~/Content/app-assets/vendors/js/tables/datatable/dataTables.rowReorder.min.js"
                      , "~/Content/app-assets/vendors/js/forms/icheck/icheck.min.js"
                      ));

            //==========================================Start:  Front End CSS ===================================================

            //bundles.Add(new StyleBundle("~/FrontContent/css").Include(
            //          "~/Content/front-content/css/font-awesome.min.css",
            //          "~/Content/front-content/css/style.css",
            //          "~/Content/front-content/css/materialize.css",
            //          "~/Content/front-content/css/bootstrap.css",
            //          "~/Content/front-content/css/mob.css",
            //          "~/Content/front-content/css/animate.css"));

            //bundles.Add(new ScriptBundle("~/bundles/JS").Include(
            //          "~/Scripts/frontjs/jquery-latest.min.js",
            //          "~/Scripts/frontjs/jquery-ui.js",
            //          "~/Scripts/frontjs/bootstrap.js",
            //          "~/Scripts/frontjs/wow.min.js",
            //          "~/Scripts/frontjs/materialize.min.js",
            //          "~/Scripts/frontjs/custom.js"));

            bundles.Add(new StyleBundle("~/FrontContent/css/above-fold").Include(
                     "~/Content/front-content/css/style.css",
                     "~/Content/front-content/css/materialize.css",
                     "~/Content/front-content/css/bootstrap.css",
                     "~/Content/front-content/css/mob.css"));

            bundles.Add(new StyleBundle("~/FrontContent/css/below-fold").Include(
                      "~/Content/front-content/css/font-awesome.min.css",
                      "~/Content/front-content/css/animate.css"));


            bundles.Add(new ScriptBundle("~/bundles/JS/above-fold").Include(
                 "~/Scripts/frontjs/jquery-latest.min.js",
                     "~/Scripts/frontjs/jquery-ui.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/JS/below-fold").Include(

                     "~/Scripts/frontjs/bootstrap.js",
                     "~/Scripts/frontjs/wow.min.js",
                     "~/Scripts/frontjs/materialize.min.js",
                     "~/Scripts/frontjs/custom.js"));

            //==========================================End:  Front End CSS ===================================================

            BundleTable.EnableOptimizations = true;
        }
    }
}
