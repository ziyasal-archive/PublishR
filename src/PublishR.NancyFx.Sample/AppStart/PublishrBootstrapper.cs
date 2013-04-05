using PublishR.NancyFx.Sample.ProductServiceReference;
using PublishR.PubSub;

namespace PublishR.NancyFx.Sample.AppStart
{
    public static class PublishrBootstrapper
    {
        public static void Init()
        {
            Publishr.Instance.Configure(ctx =>
            {
                ctx.Use<ProductServiceClient>();
                ctx.Subscribe(new Subscription("http://web3.publishr.com/publishr/",
                    typeof(ProductModule),
                   Defaults.PublishrHubName, "productCreated", "SetData"));
            });
        }
    }
}