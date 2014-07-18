using System.Collections.Generic;
using System.Reflection;
using PublishR.PubSub;

namespace PublishR.Context
{
    public interface IPublishrConfiguration
    {
        List<ISubscription> Subscriptions { get; }
        bool SendAllSubscriptionsInOneRequest { set; }
        IPublishrConfiguration WithClient<T>();
        IPublishrConfiguration WithDomain(string endPointDomain);
        IPublishrConfiguration RegisterModules(Assembly assembly);
    }
}