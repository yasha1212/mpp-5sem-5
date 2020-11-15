using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DependencyKeyAttribute : Attribute
    {
        public ImplementationName ImplementationName { get; }

        public DependencyKeyAttribute(ImplementationName name)
        {
            ImplementationName = name;
        }
    }
}
