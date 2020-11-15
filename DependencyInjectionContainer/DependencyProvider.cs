using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace DependencyInjectionContainer
{
    public class DependencyProvider
    {
        private readonly Dictionary<Type, List<ImplementationInfo>> dependencies;

        public DependencyProvider(DependenciesConfiguration configuration)
        {
            dependencies = configuration.Dependencies;
        }

        public T Resolve<T>() where T: class
        {
            Type dependencyType = typeof(T);

            return (T)Resolve(dependencyType);
        }

        private object Resolve(Type dependencyType)
        {
            if (dependencyType.IsGenericType && (dependencyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return GetAllImplementations(dependencyType.GetGenericArguments()[0]);
            }

            if (dependencyType.IsGenericType && dependencies.ContainsKey(dependencyType.GetGenericTypeDefinition()))
            {
                var implementationType = dependencies[dependencyType.GetGenericTypeDefinition()][0].ImplementationType;
                implementationType = implementationType.MakeGenericType(dependencyType.GetGenericArguments());

                if (dependencyType.IsAssignableFrom(implementationType))
                {
                    return CreateInstance(implementationType);
                }
            }

            if (!dependencies.ContainsKey(dependencyType))
            {
                return null;
            }

            var implementationInfo = dependencies[dependencyType][0];

            if (!dependencyType.IsAssignableFrom(implementationInfo.ImplementationType))
            {
                return null;
            }

            if (implementationInfo.Lifetime == Lifetime.Transient)
            {
                return CreateInstance(implementationInfo.ImplementationType);
            }

            if (implementationInfo.Lifetime == Lifetime.Singleton)
            {
                return Singleton.GetInstance(implementationInfo.ImplementationType, CreateInstance);
            }

            return null;
        }

        private IEnumerable GetAllImplementations(Type dependencyType)
        {
            List<ImplementationInfo> implementations = dependencies[dependencyType];
            Type collectionType = typeof(List<>).MakeGenericType(dependencyType);
            IList instances = (IList)Activator.CreateInstance(collectionType);

            foreach (var implementation in implementations)
            {
                instances.Add(CreateInstance(implementation.ImplementationType));
            }

            return instances;
        }

        private object CreateInstance(Type type)
        {
            ConstructorInfo constructor = type.GetConstructors()[0];
            var parameters = new List<object>();

            foreach (var parameter in constructor.GetParameters())
            {
                parameters.Add(Resolve(parameter.ParameterType));
            }

            return Activator.CreateInstance(type, parameters.ToArray());
        }
    }
}
