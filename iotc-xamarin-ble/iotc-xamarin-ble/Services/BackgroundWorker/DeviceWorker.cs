using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iotc_ble_xamarin;
using iotc_ble_xamarin.Bluetooth;
using iotc_csharp_device_client;
using iotc_csharp_device_client.enums;
using iotc_xamarin_ble.Extensions;
using iotc_xamarin_ble.Messages;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Services.BackgroundWorker
{
    public class DeviceWorker : IWorker
    {
        private IoTCClient DeviceClient { get; set; }
        private BLEService BLEService { get; }
        public IDevice BLEDevice { get; private set; }
        public Dictionary<string, string> TelemetryMap { get; private set; }

        public DeviceWorker()
        {
            BLEService = new BLEService
            {
                OnValueAvailable = OnDataAvailable
            };
        }
        public async Task ConnectIoTC(string deviceId, string scopeId, string symKey)
        {
            DeviceClient = new IoTCClient(deviceId, scopeId, IoTCConnect.SYMM_KEY, symKey);
            await DeviceClient.Connect();
            MessagingCenter.Send(new ResultMessage<IoTCConnectionState>(IoTCConnectionState.CONNECTION_OK), Constants.IOTC_DEVICE_CLIENT_CONNECTED);

        }

        public void Disconnect()
        {
            Task.Run(async () =>
            {
                await DeviceClient.Disconnect(null);
            });
        }

        public async void OnDataAvailable(object sender, CharacteristicUpdatedEventArgs e)
        {
            var pair = new GattPair(e.Characteristic);
            var measureField = TelemetryMap[pair.GattKey];
            var value = e.Characteristic.GetValue();
            //XamarinDevice.BeginInvokeOnMainThread(() =>
            //{
            //    FormattedText.Spans.Add(new Span { Text = $"Sending {measureField}={value}\n", ForegroundColor = Color.Green });
            //});
            //TODO only if device connected
            await DeviceClient.SendTelemetry($"{{\"{measureField}\":{value}}}", null);
        }

        public async Task SetupNotifications(string bleDeviceId, Dictionary<string, string> telemetryMap)
        {
            BLEDevice = await BLEService.Connect(bleDeviceId);
            TelemetryMap = telemetryMap;
            foreach (var gatt in telemetryMap)
            {
                var telemetryField = gatt.Value;
                var pair = new GattPair(gatt.Key);
                var service = await BLEDevice.GetServiceAsync(pair.ServiceId);
                if (service != null) // service could be null if mapping has old values related to other devices
                {
                    var characteristic = await service.GetCharacteristicAsync(pair.CharacteristicId);
                    if (characteristic != null) // like above but for characteristic
                    {
                        if (telemetryField != null)
                        {
                            await BLEService.EnableNotification(characteristic);
                        }
                        else
                        {
                            await BLEService.DisableNotification(characteristic);
                        }
                    }
                }
            }
            MessagingCenter.Send(new ResultMessage<IDevice>(BLEDevice), Constants.BLE_DEVICE_READY);
        }

        public void Start(string scopeId, string symKey, string deviceId, string bleDeviceId, Dictionary<string, string> telemetryMap)
        {
            new Thread(async () =>
            {
                await ConnectIoTC(deviceId, scopeId, symKey);
                await SetupNotifications(bleDeviceId, telemetryMap);
            }).Start();
        }
    }
}
