using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            ModuleAndHandlesRegistry = new ConcurrentDictionary<Type, IEnumerable<string>>();
        }

        public GlobalRegistry()
            : this(new Reflector()) { }

        public static IGlobalRegistry Instance
        {
            get { return GlobalRegistryInstance.Value; }
        }

        private ConcurrentDictionary<Type, IEnumerable<string>> ModuleAndHandlesRegistry { get; set; }

        public void RegisterModules(Assembly assemblyToScan)
        {
            ModuleAndHandlesRegistry = _reflector.GetModuleAndHandles(assemblyToScan);
        }

        public IEnumerable<MethodExecutionDefinition> FindByMessageType(string handleType)
        {
            return (from moduleAndHandles in ModuleAndHandlesRegistry
                    where moduleAndHandles.Value.Any(item => item == handleType)
                    select _reflector.GetTargetMethod(moduleAndHandles.Key, handleType)
                ).ToList();
        }
    }
}