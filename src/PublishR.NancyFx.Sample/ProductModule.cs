using Microsoft.AspNet.SignalR;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;

namespace PublishR.NancyFx.Sample
{
    public class ProductModule : PublishrModule,
        IHandle<ProductCreatedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.Invoke(message.HubMethod,new { msg = string.Format("Product created id:{0}", message.ProductId) });
        }
    }
}