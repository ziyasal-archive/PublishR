using System.Web;
using System.Web.Routing;
using PublishR.Mvc.BCommerce.App_Start;
using PublishR.Web.Mvc;

[assembly: PreApplicationStartMethod(typeof(RegisterHubs), "Start")]
namespace PublishR.Mvc.BCommerce.App_Start
{
    public static class RegisterHubs
    {
        public static void Start()
        {
            // Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();
            RouteTable.Routes.MapPublishR();

        }
    }
}