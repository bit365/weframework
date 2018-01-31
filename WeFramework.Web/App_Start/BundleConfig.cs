using System.Web.Optimization;

namespace WeFramework.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Microsoft Original Code
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
            #endregion

            RegisterStyleBundles(bundles);

            RegisterScriptBundles(bundles);

            bundles.UseCdn = BundleTable.EnableOptimizations = true;
        }

        public static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles/dashboard").Include("~/Content/dashboard.css"));
            bundles.Add(new StyleBundle("~/styles/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/styles/adminlte").Include("~/Content/adminlte.css", "~/Content/adminlte-skins.css"));
            bundles.Add(new StyleBundle("~/styles/font-awesome", @"//cdn.bootcss.com/font-awesome/4.4.0/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/styles/ionicons", @"//cdn.bootcss.com/ionicons/2.0.1/css/ionicons.min.css"));
            bundles.Add(new StyleBundle("~/styles/icheck", @"//cdn.bootcss.com/iCheck/1.0.2/skins/all.css"));
            bundles.Add(new StyleBundle("~/styles/bootstrap-datetimepicker", @"//cdn.bootcss.com/bootstrap-datetimepicker/4.17.37/css/bootstrap-datetimepicker.min.css"));
        }

        public static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/jquery", @"//apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/scripts/adminlte").Include("~/Scripts/adminlte.js", "~/Scripts/adminlte-customize.js"));
            bundles.Add(new ScriptBundle("~/scripts/html5shiv", @"//apps.bdimg.com/libs/html5shiv/3.7/html5shiv.min.js"));
            bundles.Add(new ScriptBundle("~/scripts/respond", @"//apps.bdimg.com/libs/respond.js/1.4.2/respond.js").Include("~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/scripts/icheck", @"//cdn.bootcss.com/iCheck/1.0.2/icheck.min.js"));
            bundles.Add(new ScriptBundle("~/scripts/fastclick", @"//cdn.bootcss.com/fastclick/1.0.6/fastclick.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery-unobtrusive-ajax").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery-unobtrusive-ajax-paging").Include("~/Scripts/jquery.unobtrusive-ajax-paging.js"));
            bundles.Add(new ScriptBundle("~/scripts/angular", @"//apps.bdimg.com/libs/angular.js/1.4.6/angular.min.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery-scroll-box").Include("~/Scripts/jquery.scrollbox.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery-validate-unobtrusive-bootstrap").Include("~/Scripts/jquery.validate*").GeneralOrder());
            bundles.Add(new ScriptBundle("~/scripts/bootbox", @"//cdn.bootcss.com/bootbox.js/4.4.0/bootbox.min.js"));
            bundles.Add(new ScriptBundle("~/scripts/dashboard").Include("~/Scripts/dashboard.js"));
            bundles.Add(new ScriptBundle("~/scripts/bootstrap-datetimepicker", "//cdn.bootcss.com/bootstrap-datetimepicker/4.17.37/js/bootstrap-datetimepicker.min.js"));
        }
    }
}
