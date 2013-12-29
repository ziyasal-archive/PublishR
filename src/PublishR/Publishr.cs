using System;
using PublishR.Context;
using PublishR.Messaging;
using PublishR.PubSub;
using PublishR.Registry;

namespace PublishR
{
    public class Publishr
    {
        private static readonly Lazy<Publishr> PublishrInstance = new Lazy<Publishr>(() => new Publishr(), true);

        private readonly PubSubContext _pubSubContext;

        public Publishr()
        {
            _pubSubContext = new PubSubContext();
        }

        public static Publishr Instance
        {
            get { return PublishrInstance.Value; }
        }

        public void Publish(IPublishrMessage message)
        {
            _pubSubContext.Publish(message);
        }

        public bool Exist(ISubscription subscription)
        {
            return _pubSubContext.Exist(subscription);
        }

        public void Configure(Action<IPublishrConfiguration> initializer)
        {
            initializer(_pubSubContext.Configuration);

            _pubSubContext.Init();

            GlobalRegistry.Instance.RegisterModules();
        }

        public bool UnSubscribe(ISubscription subscription)
        {
            return _pubSubContext.UnSubscribe(subscription);
        }

        public void Subscribe(ISubscription subscription)
        {
            _pubSubContext.Subscribe(subscription);
        }
    }
}