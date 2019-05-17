using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.ViewModels.Bluetooth
{
    public class BluetoothCharacteristicViewModel
    {

        public BluetoothCharacteristicViewModel(ICharacteristic characteristic)
        {
            Characteristic = characteristic;
        }

        public ICharacteristic Characteristic { get; private set; }

        public string Id
        {
            get { return Characteristic.Id.ToString(); }
        }

        public string Name
        {
            get { return Characteristic.Name; }
        }
    }
}
