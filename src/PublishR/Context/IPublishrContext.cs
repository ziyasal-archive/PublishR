using PublishR.Messaging;
using PublishR.PubSub;

namespace PublishR.Context
{
    public interface IPublishrContext
    {
        bool UnSubscribe(ISubscription subscription);
        bool Exist(ISubscription subscription);
        void Publish(IPublishrMessage message, string currentMethodName);
        void Init();
    }
}