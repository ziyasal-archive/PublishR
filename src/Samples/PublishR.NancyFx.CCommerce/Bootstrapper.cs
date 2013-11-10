using System.Web.Routing;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Nancy.Bootstrappers.Autofac;
using Nancy.Session;
using PublishR.NancyFx.CCommerce.AppStart;

namespace PublishR.NancyFx.CCommerce
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(Autofac.ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
            base.ApplicationStartup(container, pipelines);

            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
            RouteTable.Routes.MapHubs();
            PublishrBootstrapper.Init();
        }
    }
}