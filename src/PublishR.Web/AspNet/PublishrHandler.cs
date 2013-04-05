using System.Web;
using PublishR.Handlers;
using PublishR.Infrastructure;
using PublishR.Messaging;

namespace PublishR.Web.AspNet
{
    public abstract class PublishrHandler : PublishrHandlerBase, IHttpHandler
    {
        protected PublishrHandler()
            : this(new MethodScanner())
        {

        }

        protected PublishrHandler(IMethodScanner methodScanner)
        {
            MethodScanner = methodScanner;
        }

        public void ProcessRequest(HttpContext context)
        {
            string json = context.Request.Form.Get("publishr");
            var message = ServiceStack.Text.JsonSerializer.DeserializeFromString<PublishrMessage>(json);
            message.Raw = json;

            if (Correlate())
            {
                Handle(message);
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}