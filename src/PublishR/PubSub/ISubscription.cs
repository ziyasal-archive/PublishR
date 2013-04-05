using System.Collections.Generic;

namespace PublishR.PubSub
{
    public interface ISubscription
    {
        string CallbackUrl { get; }
        string SubId { get; }
        string HubName { get; }
        string HubMethod { get; }
        string ServiceMethod { get; set; }
        List<string> Handles { get; }
    }
}