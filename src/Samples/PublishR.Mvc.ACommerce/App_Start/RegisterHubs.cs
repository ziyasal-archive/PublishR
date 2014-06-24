using System.Web;
using System.Web.Routing;
using PublishR.Mvc.ACommerce;

[assembly: PreApplicationStartMethod(typeof(RegisterHubs), "Start")]
namespace PublishR.Mvc.ACommerce
{
    public static class RegisterHubs
    {
        public static void Start()
        {
            // Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();
        }
    }
}