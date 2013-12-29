using System.Web;

namespace PublishR.Helper
{
    public static class IdGenerator
    {
        public static string GenerateFrom(string callbackUrl, string serviceMethod, string hubName, string hubMethod)
        {
            string subId;

            if (HttpContext.Current.Session != null && !string.IsNullOrWhiteSpace(HttpContext.Current.Session.SessionID))
            {
                subId = HttpContext.Current.Session.SessionID;
            }
            else
            {
                subId = string.Format("{0}_{1}_{2}_{3}", callbackUrl, serviceMethod, hubName, hubMethod);
            }

            return StringEncoder.ConvertBase64String(subId);
        }
    }
}