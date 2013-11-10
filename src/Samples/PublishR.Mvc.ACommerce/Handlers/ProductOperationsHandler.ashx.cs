using Microsoft.AspNet.SignalR;
using PublishR.Hubs;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.ACommerce.Handlers
{
    public class ProductOperationsHandler : PublishrHandler,
        IHandle<ProductCreatedMessage>,
        IHandle<ProductUpdatedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            //Get hub eith hub name
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.Invoke(message.HubMethod, message);
        }

        public void Handle(ProductUpdatedMessage message)
        {
            //Use current hub property
            CurrentHubContext.Clients.All.ProductUpdated(new { Message = "Product Update handled in ProductOperationsHandler.", message.ProductId });
        }

        public void Handle(ProductDeletedMessage message)
        {
            //Get hub with hub type
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PublishrHub>();
            hubContext.Clients.All.ProductDeleted(new { Message = "Product delete handled in ProductOperationsHandler.", message.ProductId });
        }
    }
}