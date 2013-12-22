using System;
using System.Collections.Generic;
using System.Web;
using PublishR.Helper;
using PublishR.Reflection;

namespace PublishR.PubSub {
    [Serializable]
    public class Subscription : ISubscription {
        public Subscription(Type handlerType, string hubName, string hubMethod) {
            HubName = hubName;
            HubMethod = hubMethod;

            InitsubId();
            Handles = new Reflector().GetGenericInterfaceArguments(handlerType); /*TODO: IoC> - DI*/
        }

        public Subscription( Type handlerType, string hubName)
            : this(handlerType, hubName, Defaults.PUBLISHR_HUB_METHOD) {
        }

        public Subscription(Type handlerType)
            : this(handlerType, Defaults.PUBLISHR_HUB_NAME, Defaults.PUBLISHR_HUB_METHOD) {
        }

        /*TODO: Id generator*/
        private void InitsubId() {
            string subId;

            if (HttpContext.Current.Session != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session.SessionID)) {
                subId = HttpContext.Current.Session.SessionID;
            } else {
                subId = string.Format("{0}_{1}_{2}_{3}", CallbackUrl, ServiceMethod, HubName, HubMethod);
            }

            SubId = StringEncoder.ConvertBase64String(subId);
        }

        public string CallbackUrl { get; private set; }
        public string SubId { get; private set; }
        public string HubName { get; private set; }
        public string HubMethod { get; private set; }
        public string ServiceMethod { get; set; }
        public List<string> Handles { get; private set; }
    }
}
