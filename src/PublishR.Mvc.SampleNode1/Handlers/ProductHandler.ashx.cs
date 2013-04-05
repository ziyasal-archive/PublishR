using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.SampleNode1.Handlers
{
    public class ProductHandler : PublishrHandler, 
        IHandle<ProductCreatedMessage>
    {
        public void Handle(ProductCreatedMessage message)
        {
            CurrentHubContext.Clients.All.Invoke(message.HubMethod, message);
        }
    }
}