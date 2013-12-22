using System;

namespace PublishR.DI {
    public class DependencyResolveFailedException : Exception {
        public DependencyResolveFailedException(Type type) {
            
        }
    }
}