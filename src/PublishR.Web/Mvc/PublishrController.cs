using System.Web.Mvc;
using System.Web.Routing;
using PublishR.Handlers;

namespace PublishR.Web.Mvc
{
    public abstract class PublishrController :PublishrHandlerBase, IController
    {
        public void Execute(RequestContext requestContext)
        {
            /*TODO:*/
        }
    }
}