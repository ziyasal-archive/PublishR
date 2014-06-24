using PublishR.Mvc.BCommerce.Controllers;
using PublishR.Mvc.BCommerce.Modules;
using PublishR.Mvc.BCommerce.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.Mvc.BCommerce
{
    public class Bootstrapper
    {
        public static void Init()
        {
            Publishr.Instance.Configure(ctx =>
            {
                ctx.RegisterModules(typeof(HomeController).Assembly);
                ctx.Use<ProductServiceClient>();
                ctx.WithDomain("http://bcommerce.com/");
                ctx.Subscriptions.Add(new Subscription(typeof(ProductOperationsModule), Defaults.PUBLISHR_HUB_NAME, "logMessage"));
            });
        }
    }
}