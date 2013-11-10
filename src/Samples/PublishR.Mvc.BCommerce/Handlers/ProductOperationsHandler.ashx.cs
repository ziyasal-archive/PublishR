using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.BCommerce.Handlers
{
    public class ProductOperationsHandler : PublishrHandler, IHandle<ProductCreatedMessage>
    {
        public void Handle(ProductCreatedMessage message) {
            CurrentHubContext.Clients.All.Invoke(message.HubMethod, new { Message = "Product Created.", message.ProductId });
        }
    }
}