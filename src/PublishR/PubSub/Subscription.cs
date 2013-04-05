using System;
using System.Collections.Generic;
using PublishR.Helper;
using PublishR.Infrastructure;

namespace PublishR.PubSub
{
    [Serializable]
    public class Subscription : ISubscription
    {
        public Subscription(string url, Type handlerType, string hubName, string hubMethod, string serviceMethod)
        {
            CallbackUrl = url;
            HubName = hubName;
            HubMethod = hubMethod;
            ServiceMethod = serviceMethod;

            SubId = StringEncoder.ConvertBase64String(string.Format("{0}_{1}_{2}_{3}", CallbackUrl, ServiceMethod, HubName, HubMethod));
            Handles = new MethodScanner().GetGenericInterfaceArguments(handlerType); /*TODO: IoC> - DI*/
        }

        public string CallbackUrl { get; private set; }
        public string SubId { get; private set; }
        public string HubName { get; private set; }
        public string HubMethod { get; private set; }
        public string ServiceMethod { get; set; }
        public List<string> Handles { get; private set; }
    }
}
