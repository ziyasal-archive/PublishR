using System.Collections.Generic;
using PublishR.PubSub;

namespace PublishR.Context {
    public interface IPublishrConfiguration {
        void Use<T>();
        List<ISubscription> Subscriptions { get; }
        bool SendAllSubscriptionsOneCall { set; }
    }
}