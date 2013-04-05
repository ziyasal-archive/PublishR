using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PublishR.Messaging;

namespace PublishR.Infrastructure
{
    public class MethodScanner : IMethodScanner
    {
        public MethodExecutionDefination GetTargetMethod(Type type, string handleType)
        {
            MethodExecutionDefination result = new MethodExecutionDefination();
            MethodInfo[] methodInfos = type.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                ParameterInfo firstOrDefault = parameterInfos.FirstOrDefault(parameterInfo => parameterInfo.ParameterType.FullName == handleType);


                if (firstOrDefault != null)
                {
                    result.Method = methodInfo;
                    result.ParameterType = firstOrDefault.ParameterType;
                    break;
                }
            }

            return result;
        }

        public List<string> GetGenericInterfaceArguments(Type handlerType)
        {
            List<string> result = new List<string>();
            if (handlerType != null)
            {
                IEnumerable<Type> handleInterfaces = handlerType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));
                foreach (Type handle in handleInterfaces)
                {
                    Type type = handle.GenericTypeArguments.FirstOrDefault();
                    if (type != null)
                    {
                        result.Add(type.FullName);
                    }
                }
            }

            return result;
        }
    }
}