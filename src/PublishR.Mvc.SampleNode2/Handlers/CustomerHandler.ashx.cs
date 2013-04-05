using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.SampleNode2.Handlers
{
    public class CustomerHandler : PublishrHandler, IHandle<ProductUpdatedMessage>
    {
        public void Handle(ProductUpdatedMessage message)
        {
            CurrentHubContext.Clients.All.Invoke(message.HubMethod, new { Message = "Prodcut Updated.", message.ProductId });
        }
    }
}