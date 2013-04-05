using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PublishR.Logging;
using PublishR.Messaging;
using PublishR.PubSub;
using RestSharp;

namespace PublishR.Context
{
    public class PublishrContext : IPublishrContext, IPublishrSubscriptionContext
    {
        private readonly ILogger _logger;
        private readonly List<ISubscription> _subscriptions;

        public PublishrContext()
            : this(new NullLogger())
        {

        }
        public PublishrContext(ILogger logger)
        {
            _logger = logger;
            _subscriptions = new List<ISubscription>();
        }

        public bool UnSubscribe(ISubscription subscription)
        {
            return _subscriptions.Remove(subscription);
        }

        public bool Exist(ISubscription subscription)
        {
            return _subscriptions.Exists(sub => sub.SubId == subscription.SubId);
        }

        public void Publish(IPublishrMessage message, string currentMethodName)
        {
            string handleType = message.GetType().FullName;
            var filtered = _subscriptions.Where(subscription => subscription.ServiceMethod == currentMethodName && subscription.Handles.Contains(handleType));

            foreach (Subscription subscription in filtered)
            {
                message.HubName = subscription.HubName;
                message.HubMethod = subscription.HubMethod;

                message.HandleType = handleType;
                PublishImpl(subscription, message);
            }
        }

        public void Subscribe(ISubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        public Type EndpointClientType { get; set; }

        public void Use<T>()
        {
            EndpointClientType = typeof(T);
        }

        public void Init()
        {
            object instance = Activator.CreateInstance(EndpointClientType);
            MethodInfo subscriber = instance.GetType().GetMethod("Subscribe");
            MethodInfo closer = instance.GetType().GetMethod("Close");
            try
            {
                foreach (var subscription in _subscriptions)
                {
                    subscriber.Invoke(instance, new object[] { subscription });
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
            var client = new RestClient(subscription.CallbackUrl);
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("publishr", ServiceStack.Text.JsonSerializer.SerializeToString(value));

            client.Execute(request);
        }
    }
}