using Nancy;
using PublishR.NancyFx.Sample.ProductServiceReference;

namespace PublishR.NancyFx.Sample
{
    public class CallServiceModule : NancyModule
    {
        public CallServiceModule()
            : base("/svc")
        {
            Get["/{message}"] = p =>
                                    {
                                        ProductServiceClient client = new ProductServiceClient();
                                        client.SetData(31);
                                        return Response.AsJson(new { message = "Service Method Invoked.." });
                                    };
        }
    }
}