using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.BCommerce.Handlers
{
    public class ProductOperationsHandler : PublishrHandler, IHandle<ProductUpdatedMessage>
    {
        public void Handle(ProductUpdatedMessage message)
        {
            CurrentHubContext.Clients.All.Invoke(message.HubMethod, new { Message = "Product Updated.", message.ProductId });
        }
    }
}