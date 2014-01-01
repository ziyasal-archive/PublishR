using Microsoft.AspNet.SignalR;

namespace PublishR.Handlers
{
    public class PublishrModule
    {
        public IHubContext CurrentHubContext { get; set; }
    }
}