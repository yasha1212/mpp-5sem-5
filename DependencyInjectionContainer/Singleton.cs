using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer
{
    public class Singleton
    {
        private static Dictionary<Type, object> instances = new Dictionary<Type, object>();
        private static object syncLocker = new object();

        public delegate object InstanceCreator(Type type);

        public static object GetInstance(Type type, InstanceCreator createInstance)
        {
            if (!instances.ContainsKey(type))
            {
                lock (syncLocker)
                {
                    if (!instances.ContainsKey(type))
                    {
                        instances.Add(type, createInstance(type));
                    }
                }
            }

            return instances[type];
        } 
    }
}
