using System.Web.Routing;

namespace PublishR.Web.Mvc {
    public static class Routingextensions {
        public static void MapPublishR(this RouteCollection routes) {
            routes.Add(new Route("publishr", new PublishrRouteHandler()));
        }
    }
}