using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PublishR.Mvc.ACommerce.App_Start;

namespace PublishR.Mvc.ACommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            PublishrBootstrapper.Init();
        }

        void Session_Start(object sender, EventArgs e)
        {
            //TODO: session id, session close unsubscribe.
            //PublishrBootstrapper.Init();
        }
    }
}