using PublishR.Extensions;
using PublishR.Hubs;
using PublishR.Mvc.ACommerce.Modules;
using PublishR.Mvc.ACommerce.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.Mvc.ACommerce.App_Start {
    public class PublishrBootstrapper {
        public static void Init() {
            /*ForDemo: Used both type extension method and string hubname*/
            Publishr.Instance.Configure(ctx => {

                ctx.Use<ProductServiceClient>();
                ctx.WithDomain("http://acommerce.com/");
                ctx.Subscriptions.Add(new Subscription(typeof(OrderOperationsModule), typeof(PublishrHub).GetHubName()));
                ctx.Subscriptions.Add(new Subscription(typeof(ProductOperationsModule), Defaults.PUBLISHR_HUB_NAME));
            });
        }
    }
}