using Microsoft.AspNet.SignalR;

namespace PublishR.Handlers {
    public class PublishrModule {
        protected IHubContext CurrentHubContext { get; set; }
    }
}