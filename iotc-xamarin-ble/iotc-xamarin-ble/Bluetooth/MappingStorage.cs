using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Bluetooth
{
    class MappingStorage : IStorage
    {
        private static MappingStorage storage;
        private readonly Dictionary<string, string> deviceStorage;

        public static MappingStorage Current
        {
            get
            {
                if (storage == null)
                {
                    if (IoTCentral.Current.Device == null)
                    {
                        throw new Exception("No device connected or available");
                    }
                    storage = new MappingStorage();
                }
                return storage;
            }
        }

        private MappingStorage()
        {
            if (App.Current.Properties.ContainsKey(IoTCentral.Current.Device.DeviceId))
            {
                deviceStorage = new Dictionary<string, string>(App.Current.Properties[IoTCentral.Current.Device.DeviceId] as Dictionary<string, string>);
            }
            else
            {
                deviceStorage = new Dictionary<string, string>();
            }
        }

        public void Add(string key, string value)
        {
            if (!deviceStorage.ContainsKey(key))
            {
                deviceStorage.Add(key, value);
            }
            else deviceStorage[key] = value;
        }

        public async Task Save()
        {
            if (App.Current.Properties.ContainsKey(IoTCentral.Current.Device.DeviceId))
            {
                App.Current.Properties[IoTCentral.Current.Device.DeviceId] = deviceStorage;
            }
            else
            {
                App.Current.Properties.Add(IoTCentral.Current.Device.DeviceId, deviceStorage);
            }
            await App.Current.SavePropertiesAsync();
        }

        public string this[string key]
        {
            get
            {
                if (deviceStorage.ContainsKey(key))
                {
                    return deviceStorage[key];
                }
                return null;
            }
        }

        public Dictionary<string, string> GetAll()
        {
            return deviceStorage;
        }

        public void Remove(string key)
        {
            if (!deviceStorage.ContainsKey(key))
            {
                return;
            }
            else deviceStorage.Remove(key);
        }
    }
}
