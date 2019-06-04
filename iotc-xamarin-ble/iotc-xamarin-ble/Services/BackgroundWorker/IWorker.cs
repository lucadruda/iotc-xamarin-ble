using iotc_csharp_service;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Services.BackgroundWorker
{
    public interface IWorker
    {
        void Connect(string deviceId, string scopeId, string symKey, string bluetoothDeviceId);
        void Disconnect();

        void OnDataAvailable(object sender, CharacteristicUpdatedEventArgs e);

        void SetupNotifications(Dictionary<string, string> message);
    }
}
