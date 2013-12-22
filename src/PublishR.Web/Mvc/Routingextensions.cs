using System.Web.Routing;

namespace PublishR.Web.Mvc {
    public static class RoutingExtensions {
        public static void MapPublishR(this RouteCollection routes) {
            routes.Add(new Route("publishr", new PublishrRouteHandler()));
        }
    }
}