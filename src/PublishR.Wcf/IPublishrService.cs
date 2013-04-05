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
        bool UnSubscribe(Subscription subscription);
    }
}