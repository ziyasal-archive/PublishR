using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PublishR.Handlers;
using PublishR.Messaging;

namespace PublishR.Reflection
{
    public class Reflector : IReflector
    {
        public MethodExecutionDefinition GetTargetMethod(Type type, string handleType)
        {
            var result = new MethodExecutionDefinition { OwnerType = type };
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
            var result = new List<string>();
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

        public ConcurrentDictionary<Type, IEnumerable<string>> GetModuleAndHandles(Assembly assemblyToScan)
        {
            var result = new ConcurrentDictionary<Type, IEnumerable<string>>();
            List<Type> moduleTypes = new List<Type>();
            foreach (Type type in assemblyToScan.GetTypes())
            {
                if (typeof(PublishrModule).IsAssignableFrom(type))
                {
                    moduleTypes.Add(type);
                }
            }

            foreach (Type moduleType in moduleTypes)
            {
                IEnumerable<string> handles = GetGenericInterfaceArguments(moduleType);

                if (handles.Any())
                {
                    result.TryAdd(moduleType, handles);
                }
            }

            return result;
        }
    }
}