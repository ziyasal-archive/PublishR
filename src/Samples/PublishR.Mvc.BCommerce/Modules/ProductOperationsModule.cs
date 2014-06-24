using PublishR.Handlers;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;

namespace PublishR.Mvc.BCommerce.Modules
{
    public class ProductOperationsModule : PublishrModule,
        IHandle<ProductCreatedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            CurrentHubContext.Clients.All.Invoke(message.HubMethod, new { Message = "Product Created.", message.ProductId });
        }
    }
}