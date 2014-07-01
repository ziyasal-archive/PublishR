using System.Collections.Generic;
using System.Reflection;
using PublishR.PubSub;

namespace PublishR.Context
{
    public interface IPublishrConfiguration
    {
        List<ISubscription> Subscriptions { get; }
        bool SendAllSubscriptionsInOneRequest { set; }
        void WithClient<T>();
        void WithDomain(string endPointDomain);
        void RegisterModules(Assembly assembly);
    }
}