using System.Collections.Generic;
using PublishR.Reflection;

namespace PublishR.Registry
{
    public interface IGlobalRegistry
    {
        void RegisterModules();
        IEnumerable<MethodExecutionDefination> FindByMessageType(string handleType);
    }
}