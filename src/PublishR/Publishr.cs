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
        private readonly PublishrContext _publishrContext;

        public Publishr()
        {
            _publishrContext = new PublishrContext();
        }

        public void Publish(IPublishrMessage message, string methodName)
        {
            _publishrContext.Publish(message, methodName);
        }

        public bool Exist(ISubscription subscription)
        {
            return _publishrContext.Exist(subscription);
        }

        public void Configure(Action<IPublishrSubscriptionContext> initializer)
        {
            initializer(_publishrContext);
            _publishrContext.Init();
        }

        public bool UnSubscribe(ISubscription subscription)
        {
            return _publishrContext.UnSubscribe(subscription);
        }

        public void Subscribe(ISubscription subscription)
        {
            _publishrContext.Subscribe(subscription);
        }
    }
}