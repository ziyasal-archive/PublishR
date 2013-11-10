using System;
using PublishR.Context;
using PublishR.Messaging;
using PublishR.PubSub;

namespace PublishR
{
    public class Publishr
    {
        static readonly Lazy<Publishr> PublishrInstance = new Lazy<Publishr>(() => new Publishr(), true);
        public static Publishr Instance { get { return PublishrInstance.Value; } }

        private readonly PubSubContext _pubSubContext;

        public Publishr()
        {
            _pubSubContext = new PubSubContext();
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