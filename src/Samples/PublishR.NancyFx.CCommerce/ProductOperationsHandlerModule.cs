using Microsoft.AspNet.SignalR;
using PublishR.Messaging;
using PublishR.Sample.MessageLibrary;

namespace PublishR.NancyFx.CCommerce {
    public class ProductOperationsHandlerModule : PublishrModule,
        IHandle<ProductCreatedMessage>,
        IHandle<ProductDeletedMessage> {

        public void Handle(ProductCreatedMessage message) {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.productCreated(new { msg = string.Format("Product created id:{0}", message.ProductId) });
        }

        public void Handle(ProductDeletedMessage message) {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            hubContext.Clients.All.productDeleted(new { msg = string.Format("Product Deleted id:{0}", message.ProductId) });
        }
    }
}