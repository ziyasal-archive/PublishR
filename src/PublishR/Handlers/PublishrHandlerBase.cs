using Microsoft.AspNet.SignalR;
using PublishR.Messaging;
using PublishR.Reflection;

namespace PublishR.Handlers
{
    public abstract class PublishrHandlerBase
    {
        protected IReflector Reflector { get; set; }
        protected IHubContext CurrentHubContext { get; private set; }

        protected virtual void Handle(IPublishrMessage message)
        {
            CurrentHubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);
            MethodExecutionDefination methodExecution = Reflector.GetTargetMethod(GetType(), message.HandleType);
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