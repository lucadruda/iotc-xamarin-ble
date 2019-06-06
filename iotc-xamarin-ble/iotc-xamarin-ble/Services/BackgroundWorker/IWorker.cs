using iotc_csharp_service;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services.BackgroundWorker
{
    public interface IWorker
    {
        Task ConnectIoTC(string deviceId, string scopeId, string symKey);
        void Disconnect();

        void OnDataAvailable(object sender, CharacteristicUpdatedEventArgs e);

        Task SetupNotifications(string bleDeviceId, Dictionary<string, string> message);
    }
}
