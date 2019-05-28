using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Extensions
{
    public static class BLECharacteristicsExtensions
    {
        public static float GetValue(this ICharacteristic characteristic)
        {
            return System.BitConverter.ToSingle(characteristic.Value, 0);
        }
    }
}
