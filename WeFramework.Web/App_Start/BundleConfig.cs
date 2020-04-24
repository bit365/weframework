using System.Web.Optimization;

namespace WeFramework.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
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
        }

        public static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/jquery", @"//apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/scripts/adminlte").Include("~/Scripts/adminlte.js", "~/Scripts/adminlte-customize.js"));
            bundles.Add(new ScriptBundle("~/scripts/html5shiv", @"//apps.bdimg.com/libs/html5shiv/3.7/html5shiv.min.js"));
            bundles.Add(new ScriptBundle("~/scripts/respond", @"//apps.bdimg.com/libs/respond.js/1.4.2/respond.js").Include("~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/scripts/icheck", @"//cdn.bootcss.com/iCheck/1.0.2/icheck.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery-unobtrusive-ajax").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery-unobtrusive-ajax-paging").Include("~/Scripts/jquery.unobtrusive-ajax-paging.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery-validate-unobtrusive-bootstrap").Include("~/Scripts/jquery.validate*").GeneralOrder());
            bundles.Add(new ScriptBundle("~/scripts/bootbox", @"//cdnjs.cloudflare.com/ajax/libs/bootbox.js/5.4.0/bootbox.min.js"));
            bundles.Add(new ScriptBundle("~/scripts/dashboard").Include("~/Scripts/dashboard.js"));
        }
    }
}
