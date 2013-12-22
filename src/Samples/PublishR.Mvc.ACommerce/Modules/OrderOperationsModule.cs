using PublishR.Handlers;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;

namespace PublishR.Mvc.ACommerce.Modules {
    public class OrderOperationsModule : PublishrModule,
        IHandle<OrderCreatedMessage> {
        public void Handle(OrderCreatedMessage message) {
            CurrentHubContext.Clients.All.orderCreated(message);
        }
    }
}