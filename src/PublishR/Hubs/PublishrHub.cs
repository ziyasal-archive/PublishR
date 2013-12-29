using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace PublishR.Hubs
{
    [HubName(Defaults.PUBLISHR_HUB_NAME)]
    public class PublishrHub : Hub {}
}