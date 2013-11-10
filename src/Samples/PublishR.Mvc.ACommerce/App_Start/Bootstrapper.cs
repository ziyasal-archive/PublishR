using PublishR.Extensions;
using PublishR.Hubs;
using PublishR.Mvc.ACommerce.Handlers;
using PublishR.Mvc.ACommerce.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.Mvc.ACommerce.App_Start {
    public class Bootstrapper {
        public static void Init() {
            /*ForDemo: Used both type extension method and string hubname*/
            Publishr.Instance.Configure(ctx => {

                ctx.Use<ProductServiceClient>();

                ctx.Subscriptions.Add(new Subscription("http://acommerce.com/Handlers/OrderOperationsHandler.ashx",
                    typeof(OrderOperationsHandler),
                    typeof(PublishrHub).GetHubName()));

                ctx.Subscriptions.Add(new Subscription("http://acommerce.com/Handlers/ProductOperationsHandler.ashx",
                    typeof(ProductOperationsHandler), Defaults.PUBLISHR_HUB_NAME));
            });
        }
    }
}