using System.Web.Optimization;

namespace PublishR.Mvc.SampleNode2.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}