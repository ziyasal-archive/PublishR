using System.Collections.Generic;
using System.Reflection;
using PublishR.PubSub;

namespace PublishR.Context
{
    public interface IPublishrConfiguration
    {
        List<ISubscription> Subscriptions { get; }
        bool SendAllSubscriptionsOneCall { set; }
        void Use<T>();
        void WithDomain(string endPointDomain);
        void RegisterModules(Assembly assembly);
    }
}