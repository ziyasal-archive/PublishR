using System.Web;
using System.Web.Routing;
using PublishR.Mvc.ACommerce.App_Start;

[assembly: PreApplicationStartMethod(typeof(RegisterHubs), "Start")]
namespace PublishR.Mvc.ACommerce.App_Start
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