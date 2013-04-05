using Microsoft.AspNet.SignalR;
using PublishR.Hubs;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.SampleNode1.Handlers
{
    public class ProductHandler2 : PublishrHandler,
        IHandle<ProductCreatedMessage>,
        IHandle<ProductUpdatedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.Invoke(message.HubMethod, message);
        }

        public void Handle(ProductUpdatedMessage message)
        {
            CurrentHubContext.Clients.All.ProductUpdated(new { Message = "Product Update handled in ProductHandler2.", message.ProductId });
        }

        public void Handle(ProductDeletedMessage message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PublishrHub>();
            hubContext.Clients.All.ProductDeleted(new { Message = "Product delete handled in ProductHandler2.", message.ProductId });
        }
    }
}