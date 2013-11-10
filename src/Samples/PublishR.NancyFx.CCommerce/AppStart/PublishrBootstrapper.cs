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
                ctx.Use<ProductServiceClient>();
                ctx.Subscribe(new Subscription("http://ccommerce.com/publishr/",
                    typeof(ProductOperationsModule),
                   Defaults.PublishrHubName, "productCreated", "SetData"));
            });
        }
    }
}