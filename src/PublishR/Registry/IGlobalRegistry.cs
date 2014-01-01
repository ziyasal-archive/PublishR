using System.Collections.Generic;
using System.Reflection;
using PublishR.Reflection;

namespace PublishR.Registry
{
    public interface IGlobalRegistry
    {
        void RegisterModules(Assembly assemblyToScan);
        IEnumerable<MethodExecutionDefination> FindByMessageType(string handleType);
    }
}