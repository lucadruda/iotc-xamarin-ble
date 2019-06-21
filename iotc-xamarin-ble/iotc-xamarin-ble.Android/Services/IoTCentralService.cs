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
    public class IoTCentralService : Service
    {
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {


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
            var scopeId = intent.GetStringExtra(Constants.SCOPE_ID);
            var symKey = intent.GetStringExtra(Constants.SYM_KEY);
            var deviceId = intent.GetStringExtra(Constants.DEVICE_ID);
            var bleDeviceId = intent.GetStringExtra(Constants.BLE_DEVICE);
            var telemetryMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(intent.GetStringExtra(Constants.BLE_MAPPING));
            new DeviceWorker().Start(scopeId, symKey, deviceId, bleDeviceId, telemetryMap);
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
    }

}