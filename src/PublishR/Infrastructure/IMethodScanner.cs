using System;

namespace PublishR.Infrastructure
{
    public interface IMethodScanner
    {
        MethodExecutionDefination GetTargetMethod(Type type, string handleType);
    }
}