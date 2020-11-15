using System;
using System.Collections.Generic;

namespace DependencyInjectionContainer
{
    public class DependenciesConfiguration
    {
        public Dictionary<Type, List<ImplementationInfo>> Dependencies { get; }

        public DependenciesConfiguration()
        {
            Dependencies = new Dictionary<Type, List<ImplementationInfo>>();
        }

        public void Register<TDependency, TImplementation>(Lifetime lifetime = Lifetime.Transient) where TDependency: class where TImplementation: TDependency
        {
            var implementationInfo = new ImplementationInfo(typeof(TImplementation), lifetime);

            if (Dependencies.ContainsKey(typeof(TDependency)))
            {
                Dependencies[typeof(TDependency)].Add(implementationInfo);
            }
            else
            {
                Dependencies.Add(typeof(TDependency), new List<ImplementationInfo>());
                Dependencies[typeof(TDependency)].Add(implementationInfo);
            }
        }
    }
}
