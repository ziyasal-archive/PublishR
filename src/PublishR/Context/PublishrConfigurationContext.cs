using System;
using System.Collections.Generic;
using PublishR.PubSub;

namespace PublishR.Context {
    public class PublishrConfiguration : IPublishrConfiguration {

        internal Type EndpointClientType { get; set; }
        public List<ISubscription> Subscriptions { get; private set; }
        public bool SendAllSubscriptionsOneCall { internal get; set; }
        public string EndPointDomain { get; set; }

        public void WithDomain(string endPointDomain) {
            EndPointDomain = endPointDomain;
        }

        public PublishrConfiguration() {
            Subscriptions = new List<ISubscription>();
        }

        public void Use<T>() {
            EndpointClientType = typeof(T);
        }
    }
}