using System;
using System.Collections.Generic;

namespace PublishR.Reflection
{
    public interface IReflector
    {
        MethodExecutionDefination GetTargetMethod(Type type, string handleType);
        List<string> GetGenericInterfaceArguments(Type handlerType);
        MethodExecutionDefination FindByMessageType(string handleType);
    }
}