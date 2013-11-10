using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;
using PublishR.Web.AspNet;

namespace PublishR.Mvc.ACommerce.Handlers {
    public class OrderOperationsHandler : PublishrHandler,
        IHandle<OrderCreatedMessage> {
        public void Handle(OrderCreatedMessage message) {
            CurrentHubContext.Clients.All.orderCreated(message);
        }
    }
}