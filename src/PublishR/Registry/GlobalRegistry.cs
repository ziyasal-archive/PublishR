using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using PublishR.Reflection;

namespace PublishR.Registry
{
    public class GlobalRegistry : IGlobalRegistry
    {
        private static readonly Lazy<IGlobalRegistry> GlobalRegistryInstance =
            new Lazy<IGlobalRegistry>(() => new GlobalRegistry(), true);

        private readonly IReflector _reflector;

        public GlobalRegistry(IReflector reflector)
        {
            _reflector = reflector;
            ModuleAndhandlesRegistry = new ConcurrentDictionary<Type, IEnumerable<Type>>();
        }

        public GlobalRegistry()
            : this(new Reflector()) {}

        public static IGlobalRegistry Instance
        {
            get { return GlobalRegistryInstance.Value; }
        }

        private ConcurrentDictionary<Type, IEnumerable<Type>> ModuleAndhandlesRegistry { get; set; }

        public void RegisterModules()
        {
            ModuleAndhandlesRegistry = _reflector.GetModuleAndHandles();
        }

        public IEnumerable<MethodExecutionDefination> FindByMessageType(string handleType)
        {
            return (from moduleAndHandles in ModuleAndhandlesRegistry
                where moduleAndHandles.Value.Any(item => item.FullName == handleType)
                select _reflector.GetTargetMethod(moduleAndHandles.Key, handleType)
                ).ToList();
        }
    }
}