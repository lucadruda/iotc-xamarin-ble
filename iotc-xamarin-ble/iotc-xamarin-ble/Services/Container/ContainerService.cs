using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Services.Container
{
    public class ContainerService : IContainer
    {
        private Dictionary<Type, object> deps;
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
            deps = new Dictionary<Type, object>();
        }
        public void RegisterInstance<T>(T instance)
        {
            if (deps.ContainsKey(typeof(T)))
            {
                deps[typeof(T)] = instance;
            }
            else
            {
                deps.Add(typeof(T), instance);
            }
        }

        public void RegisterType<T>()
        {
            if (deps.ContainsKey(typeof(T)))
            {
                deps[typeof(T)] = default(T);
            }
            else
            {
                deps.Add(typeof(T), default(T));
            }
        }

        public T Resolve<T>()
        {
            if (deps.ContainsKey(typeof(T)))
            {
                return (T)deps[typeof(T)];
            }
            return default;
        }
    }
}
