using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer
{
    public enum Lifetime
    {
        Singleton,
        Transient
    }

    public class ImplementationInfo
    {
        public Lifetime Lifetime { get; }
        public Type ImplementationType { get; }
        public ImplementationName Name { get; }
        
        public ImplementationInfo(Type type, Lifetime lifetime, ImplementationName name)
        {
            Lifetime = lifetime;
            ImplementationType = type;
            Name = name;
        }
    }
}
