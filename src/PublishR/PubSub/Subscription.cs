using System;
using System.Collections.Generic;
using System.Web;
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

            InitsubId();
            Handles = new MethodScanner().GetGenericInterfaceArguments(handlerType); /*TODO: IoC> - DI*/
        }

        private void InitsubId()
        {
            string subId;

            if (HttpContext.Current.Session != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session.SessionID))
            {
                subId = HttpContext.Current.Session.SessionID;
            }
            else
            {
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
