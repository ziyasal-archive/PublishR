using PublishR.Mvc.BCommerce.Handlers;
using PublishR.Mvc.BCommerce.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.Mvc.BCommerce.App_Start
{
    public class Bootstrapper
    {
        public static void Init()
        {
            Publishr.Instance.Configure(ctx =>
            {
                ctx.Use<ProductServiceClient>();
                ctx.Subscribe(new Subscription("http://bcommerce.com/Handlers/ProductOperationsHandler.ashx",
                    typeof(ProductOperationsHandler), Defaults.PublishrHubName, "logMessage", "SetData"));
            });
        }
    }
}