using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Services.Container
{
    public class ContainerService : IContainer
    {
        private Dictionary<Type, object> instances;
        private Dictionary<Type, Type> types;

        private static ContainerService _instance;

        public static ContainerService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContainerService();
                }
                return _instance;
            }
        }

        private ContainerService()
        {
            instances = new Dictionary<Type, object>();
            types = new Dictionary<Type, Type>();

        }
        public void RegisterInstance<T>(T instance)
        {
            if (instances.ContainsKey(typeof(T)))
            {
                instances[typeof(T)] = instance;
            }
            else
            {
                instances.Add(typeof(T), instance);
            }
        }

        public void RegisterType<T>(Type t) 
        {
            if (types.ContainsKey(typeof(T)))
            {
                types[typeof(T)] = t;
            }
            else
            {
                types.Add(typeof(T), t);
            }
        }

        public T Resolve<T>()
        {
            if (instances.ContainsKey(typeof(T)))
            {
                return (T)instances[typeof(T)];
            }
            return default;
        }
        public Type ResolveType<T>()
        {
            if (types.ContainsKey(typeof(T)))
            {
                return types[typeof(T)];
            }
            return default;
        }

    }
}
