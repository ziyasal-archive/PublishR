using PublishR.Extensions;
using PublishR.Hubs;
using PublishR.Mvc.SampleNode1.Handlers;
using PublishR.Mvc.SampleNode1.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.Mvc.SampleNode1.App_Start
{
    public class Bootstrapper
    {
        public static void Init()
        {
            /*ForDemo: Used both type extension method and string hubname*/
            Publishr.Instance.Configure(ctx =>
            {
                ctx.Use<ProductServiceClient>();
                ctx.Subscribe(new Subscription("http://web1.publishr.com/Handlers/ProductHandler.ashx",
                    typeof(ProductHandler),
                    typeof(PublishrHub).GetHubName(), "logMessage", "SetData"));
                ctx.Subscribe(new Subscription("http://web1.publishr.com/Handlers/ProductHandler2.ashx",
                    typeof(ProductHandler2),
                    Defaults.PublishrHubName, "productUpdated", "SetData"));
            });
        }
    }
}