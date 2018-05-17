using System.Web.Optimization;
using WeiXinTicketSystem.Properties;

namespace WeiXinTicketSystem
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //AdminLTE
            bundles.Add(new ScriptBundle(Resources.ADMINLTE_SCRIPT).Include(
                    "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",
                    "~/Content/js/AdminLTE/app.js"));
            bundles.Add(new StyleBundle(Resources.ADMINLTE_STYLE).Include(
                    "~/Content/css/AdminLTE/AdminLTE.css",
                    "~/Content/css/AdminLTE/skins/skin-blue.css"));

            //Bootstrap
            bundles.Add(new ScriptBundle(Resources.BOOTSTRAP_SCRIPT).Include(
                    "~/Content/bootstrap/js/bootstrap.js"));
            bundles.Add(new StyleBundle(Resources.BOOTSTRAP_STYLE).Include(
                    "~/Content/bootstrap/css/bootstrap.css"));

            //Jquery
            bundles.Add(new ScriptBundle(Resources.JQUERY).Include(
                    "~/Content/plugins/jQuery/jquery-2.2.3.min.js",
                    "~/Content/js/jquery.validate.min.js",
                    "~/Content/js/jquery.validate.unobtrusive.min.js",
                    "~/Content/js/jquery.unobtrusive-ajax.min.js"));

            //ICheck
            bundles.Add(new ScriptBundle(Resources.ICHECK_SCRIPT).Include(
                    "~/Content/plugins/iCheck/icheck.min.js"));
            bundles.Add(new StyleBundle(Resources.ICHECK_STYLE).Include(
                    "~/Content/plugins/iCheck/flat/blue.css"));

            //morris
            bundles.Add(new ScriptBundle(Resources.MORRIS_SCRIPT).Include(
                    "~/Content/plugins/morris/morris.min.js"));
            bundles.Add(new StyleBundle(Resources.MORRIS_STYLE).Include(
                    "~/Content/plugins/morris/morris.css"));

            //Date Picker
            bundles.Add(new ScriptBundle(Resources.DATEPICKER_SCRIPT).Include(
                    "~/Content/plugins/datepicker/bootstrap-datepicker.js",
                    "~/Content/plugins/datepicker/locales/bootstrap-datepicker.zh-CN.js"));
            bundles.Add(new StyleBundle(Resources.DATEPICKER_STYLE).Include(
                    "~/Content/plugins/datepicker/datepicker3.css"));

            //Daterange picker
            bundles.Add(new ScriptBundle(Resources.DATERANGEPICKER_SCRIPT).Include(
                    "~/Content/plugins/daterangepicker/moment.min.js",
                    "~/Content/plugins/daterangepicker/daterangepicker.js"));
            bundles.Add(new StyleBundle(Resources.DATERANGEPICKER_STYLE).Include(
                    "~/Content/plugins/daterangepicker/daterangepicker.css"));

            //Font Awesome
            bundles.Add(new StyleBundle(Resources.FONT_AWESOME).Include(
                    "~/Content/plugins/fontAwesome/css/font-awesome.min.css"));

            //Notify
            bundles.Add(new ScriptBundle(Resources.NOTIFY_SCRIPT).Include(
                    "~/Content/plugins/notify/notify.min.js",
                    "~/Content/plugins/notify/notify.admin.js"));
            bundles.Add(new StyleBundle(Resources.NOTIFY_STYLE).Include(
                    "~/Content/plugins/notify/notify.admin.css"));

            //Dynatable
            bundles.Add(new ScriptBundle(Resources.DYNATABLE_SCRIPT).Include(
                    "~/Content/plugins/dynatable/jquery.dynatable.js"));
            bundles.Add(new StyleBundle(Resources.DYNATABLE_STYLE).Include(
                    "~/Content/plugins/dynatable/jquery.dynatable.css"));

            //handlebars
            bundles.Add(new ScriptBundle(Resources.HANDLEBARS).Include(
                    "~/Content/plugins/handlebars/handlebars-v4.0.5.js"));

            //select2
            bundles.Add(new ScriptBundle(Resources.SELECT2_SCRIPT).Include(
                    "~/Content/plugins/select2/select2.full.min.js"));
            bundles.Add(new StyleBundle(Resources.SELECT2_STYLE).Include(
                    "~/Content/plugins/select2/select2.min.css"));

            //后台公共
            bundles.Add(new ScriptBundle(Resources.ADMIN_SCRIPT).Include(
                    "~/Content/plugins/dynatable/jquery.dynatable.js",
                    "~/Content/plugins/morris/morris.min.js",
                    "~/Content/plugins/daterangepicker/moment.min.js",
                    "~/Content/plugins/daterangepicker/daterangepicker.js",
                    "~/Content/plugins/datepicker/bootstrap-datepicker.js",
                    "~/Content/plugins/datepicker/locales/bootstrap-datepicker.zh-CN.js",
                    "~/Content/plugins/iCheck/icheck.min.js",
                    "~/Content/plugins/notify/notify.min.js",
                    "~/Content/plugins/notify/notify.admin.js",
                    "~/Content/plugins/handlebars/handlebars-v4.0.5.js",
                    "~/Content/plugins/select2/select2.full.min.js",
                    "~/Content/plugins/slimScroll/jquery.slimscroll.min.js",

                    //日期时间控件
                    "~/Content/plugins/bootstrap-datetimepicker-master/js/bootstrap-datetimepicker.js",
                    "~/Content/plugins/bootstrap-datetimepicker-master/js/locales/bootstrap-datetimepicker.zh-CN.js",

                    //后台添加公共Js插件请在此行以上添加
                    "~/Content/js/AdminLTE/app.js",
                    "~/Content/js/admin.js"));
            bundles.Add(new StyleBundle(Resources.ADMIN_STYLE).Include(
                    "~/Content/plugins/dynatable/jquery.dynatable.css",
                    "~/Content/plugins/iCheck/flat/blue.css",
                    "~/Content/plugins/morris/morris.css",
                    "~/Content/plugins/datepicker/datepicker3.css",
                    "~/Content/plugins/daterangepicker/daterangepicker.css",
                    "~/Content/plugins/notify/notify.admin.css",
                    "~/Content/plugins/select2/select2.min.css",

                    //日期时间控件
                    "~/Content/plugins/bootstrap-datetimepicker-master/css/bootstrap-datetimepicker.min.css",

                    //后台添加公共Css请在此行以上添加
                    "~/Content/css/AdminLTE/AdminLTE.css",
                    "~/Content/css/AdminLTE/skins/skin-blue.css",
                    "~/Content/css/admin.css"));
        }
    }
}
