using System.Collections.Generic;
using System.ServiceModel;
using PublishR.PubSub;

namespace PublishR.Wcf
{
    [ServiceContract]
    public interface IPublishrService
    {
        [OperationContract]
        void Subscribe(Subscription subscription);

        [OperationContract]
        void SubscribeAll(List<Subscription> subscriptions);

        [OperationContract]
        bool UnSubscribe(Subscription subscription);
    }
}