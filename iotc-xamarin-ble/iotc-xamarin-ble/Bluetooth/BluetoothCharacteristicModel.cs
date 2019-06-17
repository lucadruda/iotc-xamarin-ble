using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Bluetooth
{
    public class BluetoothCharacteristicModel
    {
        public BluetoothCharacteristicModel(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public BluetoothCharacteristicModel(string id, string name, string serviceId) : this(id, name)
        {
            ServiceId = serviceId;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string ServiceId { get; set; }
    }
}
