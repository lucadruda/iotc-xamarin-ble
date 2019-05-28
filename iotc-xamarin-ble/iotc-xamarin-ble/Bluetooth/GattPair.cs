using Plugin.BLE.Abstractions.Contracts;
using System;

namespace iotc_ble_xamarin.Bluetooth
{

    public class GattPair
    {
        public Guid ServiceId { get; set; }
        public Guid CharacteristicId { get; set; }

        public GattPair(Guid serviceId, Guid characteristicId)
        {
            this.ServiceId = serviceId;
            this.CharacteristicId = characteristicId;
        }

        public GattPair(string pairstring)
        {
            string[] pair = pairstring.Split('/');
            this.ServiceId = new Guid(pair[0]);
            this.CharacteristicId = new Guid(pair[1]);
        }

        public GattPair(ICharacteristic characteristic)
        {
            this.ServiceId = characteristic.Service.Id;
            this.CharacteristicId = characteristic.Id;
        }


        public string GattKey
        {
            get { return $"{this.ServiceId}/{this.CharacteristicId}"; }
        }


    }
}