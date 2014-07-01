using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace PublishR.Reflection
{
    public interface IReflector
    {
        MethodExecutionDefinition GetTargetMethod(Type type, string handleType);
        List<string> GetGenericInterfaceArguments(Type handlerType);
        ConcurrentDictionary<Type, IEnumerable<string>> GetModuleAndHandles(Assembly assemblyToScan);
    }
}