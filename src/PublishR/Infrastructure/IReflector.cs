using System;
using System.Collections.Generic;

namespace PublishR.Infrastructure
{
    public interface IReflector
    {
        MethodExecutionDefination GetTargetMethod(Type type, string handleType);
        List<string> GetGenericInterfaceArguments(Type handlerType);
    }
}