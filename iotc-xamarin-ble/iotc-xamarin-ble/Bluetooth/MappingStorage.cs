using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
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
            if (CrossSettings.Current.Contains(IoTCentral.Current.Device.DeviceId))
            {
                deviceStorage = JsonConvert.DeserializeObject<Dictionary<string, string>>(CrossSettings.Current.GetValueOrDefault(IoTCentral.Current.Device.DeviceId, "{}"));
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

        public void Save()
        {
            CrossSettings.Current.AddOrUpdateValue(IoTCentral.Current.Device.DeviceId, JsonConvert.SerializeObject(deviceStorage));
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

        public void Clear()
        {
            deviceStorage.Clear();
            CrossSettings.Current.Remove(IoTCentral.Current.Device.DeviceId);
        }
    }
}
