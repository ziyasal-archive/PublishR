using Microsoft.AspNet.SignalR;
using Nancy;
using PublishR.Hubs;
using PublishR.Infrastructure;
using PublishR.Messaging;

namespace PublishR.NancyFx
{
    public class PublishrModule : NancyModule
    {
        readonly IMethodScanner _methodScanner;

        public PublishrModule()
            : base("/publishr")
        {
            _methodScanner = new MethodScanner();/*TODO : IoC*/
            Post["/"] = p =>
            {
                if (Request.Form.publishr.HasValue)
                {
                    string json = Request.Form.publishr;
                    var message = ServiceStack.Text.JsonSerializer.DeserializeFromString<PublishrMessage>(json);
                    message.Raw = json;

                    if (Correlate())
                    {
                        Handle(message);
                    }
                }
                return null;
            };
        }

        private void Handle(IPublishrMessage message)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PublishrHub>();
            MethodExecutionDefination methodExecution = _methodScanner.GetTargetMethod(GetType(), message.HandleType);

            if (methodExecution.Method != null)
            {
                object messageObj = ServiceStack.Text.JsonSerializer.DeserializeFromString(message.Raw, methodExecution.ParameterType);
                methodExecution.Method.Invoke(this, new[] { messageObj });
            }
            else
            {
                if (hubContext != null)
                {
                    hubContext.Clients.All.Invoke(message.HubMethod, new { data = message.Raw });
                }
            }
        }

        protected virtual bool Correlate()
        {
            return true;
        }
    }
}