using PublishR.PubSub;

namespace PublishR.Context
{
    public interface IPublishrSubscriptionContext
    {
        void Use<T>();
        void Subscribe(ISubscription subscription);
    }
}