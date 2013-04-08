using PublishR.PubSub;

namespace PublishR.Wcf
{
    public class PublishrService : IPublishrService
    {
        protected Publishr Publishr { get; private set; }

        public PublishrService()
        {
            Publishr = Publishr.Instance;
        }

        public void Subscribe(Subscription subscription)
        {
            if (!Publishr.Exist(subscription))
            {
                Publishr.Subscribe(subscription);
            }
        }

        public bool UnSubscribe(Subscription subscription)
        {
            bool remove = Publishr.UnSubscribe(subscription);
            return remove;
        }
    }
}