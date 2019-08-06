using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Services.Container
{
    public interface IContainer
    {
        void RegisterInstance<T>(T instance);
        void RegisterType<T>(Type t);
        T Resolve<T>();
        Type ResolveType<T>();
    }
}
