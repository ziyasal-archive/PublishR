using Nancy;

namespace PublishR.NancyFx.Sample
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => View["Home.html"];
        }
    }
}