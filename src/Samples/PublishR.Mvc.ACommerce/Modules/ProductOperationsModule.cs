using Microsoft.AspNet.SignalR;
using PublishR.Handlers;
using PublishR.Hubs;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;

namespace PublishR.Mvc.ACommerce.Modules
{
    public class ProductOperationsModule : PublishrModule,
       IHandle<ProductCreatedMessage>,
       IHandle<ProductUpdatedMessage>,
       IHandle<ProductDeletedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            //Get hub with hub name
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.productCreated(new { Message = "Product Create handled in ProductOperationsModule.", message.ProductId });
        }

        public void Handle(ProductUpdatedMessage message)
        {
            //Use current hub property
            CurrentHubContext.Clients.All.productUpdated(new { Message = "Product Update handled in ProductOperationsModule.", message.ProductId });
        }

        public void Handle(ProductDeletedMessage message)
        {
            //Get hub with hub type
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PublishrHub>();
            hubContext.Clients.All.productDeleted(new { Message = "Product delete handled in ProductOperationsModule.", message.ProductId });
        }
    }
}