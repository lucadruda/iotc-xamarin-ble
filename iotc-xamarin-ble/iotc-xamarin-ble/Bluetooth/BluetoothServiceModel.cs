using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Bluetooth
{
    public class BluetoothServiceModel
    {
        public BluetoothServiceModel(string id, string name, IList<BluetoothCharacteristicModel> characteristics) : this(id, name)
        {
            Characteristics = characteristics;
        }

        public BluetoothServiceModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public IList<BluetoothCharacteristicModel> Characteristics { get; set; }
    }
}
