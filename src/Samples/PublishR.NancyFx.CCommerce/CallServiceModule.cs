using Nancy;
using PublishR.NancyFx.CCommerce.ProductServiceReference;

namespace PublishR.NancyFx.CCommerce
{
    public class CallServiceModule : NancyModule
    {
        public CallServiceModule()
            : base("/svc")
        {
            Get["/{message}"] = p =>
                                    {
                                        ProductServiceClient client = new ProductServiceClient();
                                    
                                        return Response.AsJson(new { message = "Service Method Invoked.." });
                                    };
        }
    }
}