using PublishR.Mvc.SampleNode2.Handlers;
using PublishR.Mvc.SampleNode2.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.Mvc.SampleNode2.App_Start
{
    public class Bootstrapper
    {
        public static void Init()
        {
            Publishr.Instance.Configure(ctx =>
            {
                ctx.Use<ProductServiceClient>();
                ctx.Subscribe(new Subscription("http://web2.publishr.com/Handlers/CustomerHandler.ashx",
                    typeof(CustomerHandler), Defaults.PublishrHubName, "logMessage", "SetData"));
            });
        }
    }
}