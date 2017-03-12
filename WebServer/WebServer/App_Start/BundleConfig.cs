using System.Web.Optimization;

namespace WebServer
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/SignalR").Include(
                        "~/Scripts/jquery.signalR-{varsion}.js"));
        }
    }
}