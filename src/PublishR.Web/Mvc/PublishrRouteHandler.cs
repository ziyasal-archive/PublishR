using System.Web;
using System.Web.Routing;
using PublishR.Reflection;

namespace PublishR.Web.Mvc {
    public class PublishrRouteHandler : IRouteHandler {
        public IHttpHandler GetHttpHandler(RequestContext requestContext) {
            return new PublishrHandler(new Reflector(), requestContext);
        }
    }
}