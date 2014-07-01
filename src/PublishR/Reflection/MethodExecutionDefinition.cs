using System;
using System.Reflection;

namespace PublishR.Reflection
{
    public class MethodExecutionDefinition
    {
        public MethodInfo Method { get; set; }
        public Type ParameterType { get; set; }
        public Type OwnerType { get; set; }
    }
}