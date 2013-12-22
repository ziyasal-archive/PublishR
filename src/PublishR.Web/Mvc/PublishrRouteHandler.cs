using System.Web;
using System.Web.Routing;
using PublishR.Reflection;
using PublishR.Web.AspNet;

namespace PublishR.Web.Mvc {
    public class PublishrRouteHandler : IRouteHandler {
        public IHttpHandler GetHttpHandler(RequestContext requestContext) {
            return new PublishrHandler(new Reflector(), requestContext);
        }
    }
}