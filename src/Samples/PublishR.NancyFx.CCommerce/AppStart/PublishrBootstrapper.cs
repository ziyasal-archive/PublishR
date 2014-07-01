using PublishR.NancyFx.CCommerce.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.NancyFx.CCommerce.AppStart
{
    public static class PublishrBootstrapper
    {
        public static void Init()
        {
            Publishr.Instance.Configure(ctx =>
            {
                ctx.WithClient<ProductServiceClient>();
                ctx.WithDomain("http://ccommerce.com/");
                ctx.Subscriptions.Add(new Subscription(typeof(ProductOperationsHandlerModule)));
            });
        }
    }
}