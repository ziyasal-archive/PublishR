using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using PublishR.Messaging;
using PublishR.Reflection;
using PublishR.Registry;
using ServiceStack.Text;

namespace PublishR.Handlers
{
    public abstract class PublishrHandlerBase
    {
        private readonly IGlobalRegistry _globalRegistry;

        protected PublishrHandlerBase(IGlobalRegistry globalRegistry)
        {
            _globalRegistry = globalRegistry;
        }

        protected IHubContext CurrentHubContext { get; private set; }

        protected virtual void Handle(IPublishrMessage message)
        {
            CurrentHubContext = GlobalHost.ConnectionManager.GetHubContext(message.HubName);

            IEnumerable<MethodExecutionDefinition> methodExecutionDefinations = _globalRegistry.FindByMessageType(message.HandleType);


            foreach (MethodExecutionDefinition methodExecution in methodExecutionDefinations)
            {
                if (methodExecution.Method != null)
                {
                    object messageObj = JsonSerializer.DeserializeFromString(message.Raw, methodExecution.ParameterType);

                    PublishrModule moduleInstance = (PublishrModule)Activator.CreateInstance(methodExecution.OwnerType);
                    moduleInstance.CurrentHubContext = CurrentHubContext;

                    methodExecution.Method.Invoke(moduleInstance, new[] { messageObj });
                }
                else
                {
                    CurrentHubContext.Clients.All.Invoke(message.HubMethod, new { data = message.Raw });
                }
            }
        }

        protected virtual bool Correlate()
        {
            return true;
        }
    }
}