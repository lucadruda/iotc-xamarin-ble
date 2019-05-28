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
            var value = characteristic.Value;
            if (value.Length == 0)
            {
                throw new Exception("Empty value");
            }
            if (value.Length == 1)
            {
                return value[0];
            }
            return System.BitConverter.ToSingle(characteristic.Value, 0);
        }
    }
}
