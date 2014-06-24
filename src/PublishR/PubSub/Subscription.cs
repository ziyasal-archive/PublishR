using System;
using System.Collections.Generic;
using PublishR.Helper;
using PublishR.Reflection;

namespace PublishR.PubSub
{
    [Serializable]
    public class Subscription : ISubscription
    {
        public Subscription(Type handlerType, string hubName, string hubMethod)
        {
            HubName = hubName;
            HubMethod = hubMethod;
            SubId = IdGenerator.GenerateFrom(CallbackUrl, ServiceMethod, HubName, HubMethod);
            Handles = new Reflector().GetGenericInterfaceArguments(handlerType); /*TODO: DI*/
        }

        public Subscription(Type handlerType, string hubName)
            : this(handlerType, hubName, Defaults.PUBLISHR_HUB_METHOD) { }

        public Subscription(Type handlerType)
            : this(handlerType, Defaults.PUBLISHR_HUB_NAME, Defaults.PUBLISHR_HUB_METHOD) { }

        public string CallbackUrl { get; set; }
        public string SubId { get; private set; }
        public string HubName { get; private set; }
        public string HubMethod { get; private set; }
        public string ServiceMethod { get; set; }
        public List<string> Handles { get; private set; }
    }
}