using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iotc_ble_xamarin;
using iotc_ble_xamarin.Bluetooth;
using iotc_csharp_device_client;
using iotc_csharp_device_client.enums;
using iotc_xamarin_ble.Extensions;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Services;
using iotc_xamarin_ble.Services.BackgroundWorker;
using Newtonsoft.Json;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Xamarin.Forms;

namespace iotc_xamarin_ble.Droid.Services
{
    [Service]
    public class IoTCentralService : Service, IWorker
    {
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        public IIoTCClient DeviceClient { get; set; }
        public IDevice BLEDevice { get; set; }
        public BLEService BLEService { get; set; }


        public Dictionary<string, string> TelemetryMap { get; set; }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            BLEService = new BLEService
            {
                OnValueAvailable = OnDataAvailable
            };

            Notification.Builder builder;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                builder = new Notification.Builder(this, "default");

            }
            else
            {
                builder = new Notification.Builder(this);
            }
            var notification = builder.SetContentTitle("Azure IoTCentral")
                .SetContentText("BLE telemetry is running. Tap to return to application")
                .SetSmallIcon(Resource.Drawable.ic_iotcentral_white)
                .SetContentIntent(GetIntentForActivityResume())
                .SetOngoing(true)
                .Build();

            Connect(intent).Start();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private PendingIntent GetIntentForActivityResume()
        {
            var intent = new Intent(Application.ApplicationContext, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            return PendingIntent.GetActivity(Application.ApplicationContext, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.OneShot);
        }

        #region IoTC

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
        #endregion


        #region Bluetooth


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

        private Thread Connect(Intent intent)
        {
            return new Thread(async () =>
           {
               var scopeId = intent.GetStringExtra(Constants.SCOPE_ID);
               var symKey = intent.GetStringExtra(Constants.SYM_KEY);
               var deviceId = intent.GetStringExtra(Constants.DEVICE_ID);
               var bleDeviceId = intent.GetStringExtra(Constants.BLE_DEVICE);
               var telemetryMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(intent.GetStringExtra(Constants.BLE_MAPPING));
               await ConnectIoTC(deviceId, scopeId, symKey);
               await SetupNotifications(bleDeviceId, telemetryMap);
           });
        }

        #endregion
    }

}