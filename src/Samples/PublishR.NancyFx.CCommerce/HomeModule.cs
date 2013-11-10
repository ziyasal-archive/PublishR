using Nancy;

namespace PublishR.NancyFx.CCommerce
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => View["Home.html"];
        }
    }
}