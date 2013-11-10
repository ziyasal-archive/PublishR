using System;
using System.Linq;
using System.Reflection;
using PublishR.Logging;
using PublishR.Messaging;
using PublishR.PubSub;
using RestSharp;

namespace PublishR.Context {
    public class PubSubContext : IPublishrContext {
        private readonly ILogger _logger;
        public PublishrConfiguration Configuration { get; set; }

        public PubSubContext()
            : this(new NullLogger()) {
            Configuration=new PublishrConfiguration();
        }
        public PubSubContext(ILogger logger) {
            _logger = logger;
        }

        public bool UnSubscribe(ISubscription subscription) {
            return Configuration.Subscriptions.Remove(subscription);
        }

        public bool Exist(ISubscription subscription) {
            return Configuration.Subscriptions.Exists(sub => sub.SubId == subscription.SubId);
        }

        public void Publish(IPublishrMessage message) {
            string handleType = message.GetType().FullName;
            var filtered = Configuration.Subscriptions.Where(subscription => subscription.Handles.Contains(handleType));

            foreach (Subscription subscription in filtered) {
                message.HubName = subscription.HubName;
                message.HubMethod = subscription.HubMethod;

                message.HandleType = handleType;
                PublishImpl(subscription, message);
            }
        }

        public void Init() {
            object instance = Activator.CreateInstance(Configuration.EndpointClientType);
            MethodInfo closer = instance.GetType().GetMethod("Close");
            try {
                MethodInfo subscriber = Configuration.SendAllSubscriptionsOneCall
                    ? instance.GetType().GetMethod("SubscribeAll")
                    : instance.GetType().GetMethod("Subscribe");

                if (Configuration.SendAllSubscriptionsOneCall) {
                    subscriber.Invoke(instance, new object[] { Configuration.Subscriptions });
                } else {
                    foreach (var subscription in Configuration.Subscriptions) {
                        subscriber.Invoke(instance, new object[] { subscription });
                    }
                }
            } catch (Exception e) {
                _logger.Log(e, string.Empty);
            } finally {
                closer.Invoke(instance, new object[] { });
            }
        }

        private void PublishImpl(Subscription subscription, IPublishrMessage value) {
            var client = new RestClient(subscription.CallbackUrl);
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("publishr", ServiceStack.Text.JsonSerializer.SerializeToString(value));

            client.Execute(request);
        }

        public void Subscribe(ISubscription subscription) {
            Configuration.Subscriptions.Add(subscription);
        }
    }
}