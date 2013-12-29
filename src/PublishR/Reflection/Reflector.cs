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
        public MethodExecutionDefination GetTargetMethod(Type type, string handleType)
        {
            var result = new MethodExecutionDefination();
            MethodInfo[] methodInfos = type.GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                ParameterInfo firstOrDefault =
                    parameterInfos.FirstOrDefault(parameterInfo => parameterInfo.ParameterType.FullName == handleType);


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
                IEnumerable<Type> handleInterfaces =
                    handlerType.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IHandle<>));
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

        public ConcurrentDictionary<Type, IEnumerable<Type>> GetModuleAndHandles()
        {
            var result = new ConcurrentDictionary<Type, IEnumerable<Type>>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> moduleTypes =
                    assembly.GetTypes().Where(item => item.IsAssignableFrom(typeof (PublishrModule)));

                foreach (Type moduleType in moduleTypes)
                {
                    result.TryAdd(moduleType, GetHandles(moduleType));
                }
            }
            return result;
        }

        private IEnumerable<Type> GetHandles(Type handlerType)
        {
            IEnumerable<Type> result = new List<Type>();

            if (handlerType != null)
            {
                result =
                    handlerType.GetInterfaces()
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IHandle<>));
            }

            return result;
        }
    }
}