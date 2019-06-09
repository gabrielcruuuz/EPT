using System.Web;
using System.Web.Optimization;

namespace EPT
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/BundlesJS").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/script.js",
                        "~/Content/Highcharts-7.0.3/highcharts.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Highcharts-7.0.3/css/highcharts.css"));
        }

    }
}
