using System;
using PublishR.Context;
using PublishR.Messaging;
using PublishR.PubSub;

namespace PublishR
{
    public class Publishr
    {
        /*TODO: Improve Context management*/
        static readonly Lazy<Publishr> PublishrInstance = new Lazy<Publishr>(() => new Publishr(), true);
        public static Publishr Instance { get { return PublishrInstance.Value; } }
        private readonly PubSubContext _pubSubContext;

        public Publishr()
        {
            _pubSubContext = new PubSubContext();
        }

        public void Publish(IPublishrMessage message, string methodName)
        {
            _pubSubContext.Publish(message, methodName);
        }

        public bool Exist(ISubscription subscription)
        {
            return _pubSubContext.Exist(subscription);
        }

        public void Configure(Action<IPublishrSubscriptionContext> initializer)
        {
            initializer(_pubSubContext);
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