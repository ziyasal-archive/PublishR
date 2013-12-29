using System.Web;
using System.Web.Routing;
using PublishR.Registry;

namespace PublishR.Web.Mvc
{
    public class PublishrRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new PublishrHandler(requestContext, GlobalRegistry.Instance);
        }
    }
}