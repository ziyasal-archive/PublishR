using Microsoft.AspNet.SignalR;
using PublishR.Infrastructure;
using PublishR.Messaging;

namespace PublishR.Handlers
{
    public abstract class PublishrHandlerBase
    {
        protected IMethodScanner MethodScanner { get; set; }
        protected IHubContext CurrentHubContext { get; private set; }

        protected virtual void Handle(IPublishrMessage message)
        {
            CurrentHubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            MethodExecutionDefination methodExecution = MethodScanner.GetTargetMethod(GetType(), message.HandleType);
            if (methodExecution.Method != null)
            {
                object messageObj = ServiceStack.Text.JsonSerializer.DeserializeFromString(message.Raw, methodExecution.ParameterType);
                methodExecution.Method.Invoke(this, new[] { messageObj });
            }
            else
            {
                CurrentHubContext.Clients.All.Invoke(message.HubMethod, new { data = message.Raw });
            }
        }

        protected virtual bool Correlate()
        {
            return true;
        }
    }
}