using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PublishR.Reflection
{
    public interface IReflector
    {
        MethodExecutionDefination GetTargetMethod(Type type, string handleType);
        List<string> GetGenericInterfaceArguments(Type handlerType);
        ConcurrentDictionary<Type, IEnumerable<Type>> GetModuleAndHandles();
    }
}