using System;
using System.Reflection;

namespace PublishR.Infrastructure
{
    public class MethodExecutionDefination
    {
        public MethodInfo Method { get; set; }
        public Type ParameterType { get; set; }
    }
}