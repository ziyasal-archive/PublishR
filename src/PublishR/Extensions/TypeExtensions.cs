using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;

namespace PublishR.Extensions
{
    public static class TypeExtensions
    {
        public static string GetHubName(this Type hubType)
        {
            string result = string.Empty;
            List<HubNameAttribute> attributes = hubType.GetCustomAttributes(typeof(HubNameAttribute), true).Cast<HubNameAttribute>().ToList();
            
            if (attributes.Count > 0)
            {
                HubNameAttribute hubNameAttribute = attributes.FirstOrDefault();
                if (hubNameAttribute != null && !string.IsNullOrWhiteSpace(hubNameAttribute.HubName))
                {
                    result = hubNameAttribute.HubName;
                }
            }
            else
            {
                result = hubType.Name;
            }

            return result;
        }
    }
}