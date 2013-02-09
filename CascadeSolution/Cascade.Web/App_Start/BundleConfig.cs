using System.Web;
using System.Web.Optimization;

namespace Cascade.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            
            //For DataTable functionality
            //bundles.Add(new StyleBundle("~/Content/datatable").Include("~/Content/dataTables/demo_table.css"));
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new StyleBundle("~/Content/themes/Menu/css").Include(
                       "~/Content/themes/Menu/*.css"));
            bundles.Add(new ScriptBundle("~/bundles/external").Include("~/Scripts/jquery.dropdownPlain.js",
                "~/Scripts/consolelog.js",
                "~/Scripts/External.js",
                "~/Scripts/application.js",
                "~/Scripts/jquery.formatCurrency-1.4.0.min.js",
                 "~/Scripts/jquery.cascade.search.js",
                "~/Scripts/plugins.js", 
                "~/Scripts/datatable/jquery-1.4.4.min.js",
                "~/Scripts/datatable/jquery.dataTables.min.js",
                "~/Scripts/datatable/index.js",
                "~/Scripts/cog.js",
                "~/Scripts/cog.utils.js",
                "~/Scripts/format.date.js",
                //"~/Scripts/jquery.formatCurrency-1.4.0.min.js",
                "~/FusionChartLib/FusionCharts.js",
                "~/FusionChartLib/FusionCharts.HC.js",
                "~/FusionChartLib/FusionCharts.jqueryplugin"));
            
            //For DataTable functionality
            //bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
            //    //"~/Scripts/datatable/jquery-1.4.4.min.js",
            //    "~/Scripts/datatable/jquery.dataTables.min.js",
            //    "~/Scripts/datatable/index.js"
            //    ));
        }
    }
}