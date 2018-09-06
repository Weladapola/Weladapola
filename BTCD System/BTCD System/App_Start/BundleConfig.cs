using System.Web;
using System.Web.Optimization;

namespace BTCD_System
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate.min.js",
                        "~/Scripts/jquery/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/bootstrap/js/bootstrap3-typeahead.min.js",
                      "~/Scripts/bootstrap/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                     "~/Scripts/adminlte/js/adminlte.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/iCheck").Include(
                     "~/Scripts/iCheck/icheck.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                     "~/Scripts/select2/js/select2.full.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/returnToTop").Include(
                         "~/Scripts/return-to-top/return-to-top.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                   "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                     "~/Content/datatables/jquery.dataTables.js",
                     //"~/Content/datatables/dataTables.bootstrap.min.js",
                     "~/Content/datatables/dataTables.rowReorder.min.js",
                     "~/Content/datatables/dataTables.responsive.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
            "~/Content/bootstrap/css/bootstrap.min.css",
            "~/Content/datatables/jquery.dataTables.min.css",
            "~/Content/datatables/rowReorder.dataTables.min.css",
            "~/Content/datatables/responsive.dataTables.min.css",
            "~/Content/font-awesome/css/font-awesome.min.css",
            "~/Content/Ionicons/css/ionicons.min.css",
            "~/Content/select2/css/select2.min.css",
            "~/Content/adminlte/css/AdminLTE.min.css",
            "~/Content/return-to-top/return-to-top.css",
            "~/Content/bootstrap/css/bootstrap-switch.min.css",
            "~/Content/timepicker/bootstrap-timepicker.min.css"));


            bundles.Add(new StyleBundle("~/bundles/blue-skin-css").Include(
                "~/Content/adminlte/css/skins/skin-blue.min.css"));

            bundles.Add(new StyleBundle("~/Content/iCheck-square").Include(
                "~/Content/plugins/square/blue.css"));


        }
    }
}
