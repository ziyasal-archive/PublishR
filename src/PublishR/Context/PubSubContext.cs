using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PublishR.Logging;
using PublishR.Messaging;
using PublishR.PubSub;
using RestSharp;
using ServiceStack.Text;

namespace PublishR.Context
{
    public class PubSubContext : IPublishrContext
    {
        private readonly ILogger _logger;

        public PubSubContext()
            : this(new NullLogger())
        {
            Configuration = new PublishrConfiguration();
        }

        public PubSubContext(ILogger logger)
        {
            _logger = logger;
        }

        public PublishrConfiguration Configuration { get; set; }

        public bool UnSubscribe(ISubscription subscription)
        {
            return Configuration.Subscriptions.Remove(subscription);
        }

        public bool Exist(ISubscription subscription)
        {
            return Configuration.Subscriptions.Exists(sub => sub.SubId == subscription.SubId);
        }

        public void Publish(IPublishrMessage message)
        {
            string handleType = message.GetType().FullName;
            IEnumerable<ISubscription> filtered =
                Configuration.Subscriptions.Where(subscription => subscription.Handles.Contains(handleType));

            foreach (Subscription subscription in filtered)
            {
                message.HubName = subscription.HubName;
                message.HubMethod = subscription.HubMethod;

                message.HandleType = handleType;
                PublishImpl(subscription, message);
            }
        }

        public void Init()
        {
            object instance = Activator.CreateInstance(Configuration.EndpointClientType);
            MethodInfo closer = instance.GetType().GetMethod("Close");
            Configuration.Subscriptions.ForEach(item => item.CallbackUrl = Configuration.EndPointDomain);
            try
            {
                MethodInfo subscriber = Configuration.SendAllSubscriptionsOneCall
                    ? instance.GetType().GetMethod("SubscribeAll")
                    : instance.GetType().GetMethod("Subscribe");

                if (Configuration.SendAllSubscriptionsOneCall)
                {
                    subscriber.Invoke(instance, new object[] { Configuration.Subscriptions });
                }
                else
                {
                    foreach (ISubscription subscription in Configuration.Subscriptions)
                    {
                        subscriber.Invoke(instance, new object[] { subscription });
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(e, string.Empty);
            }
            finally
            {
                closer.Invoke(instance, new object[] { });
            }
        }

        private void PublishImpl(Subscription subscription, IPublishrMessage value)
        {
            var client = new RestClient(subscription.CallbackUrl.TrimEnd('/') + "/publishr/connect");
            var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
            request.AddParameter("_", JsonSerializer.SerializeToString(value));

            client.Execute(request);
        }

        public void Subscribe(ISubscription subscription)
        {
            Configuration.Subscriptions.Add(subscription);
        }
    }
}