using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Plugin.Permissions;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using iotc_xamarin_ble.Messages;
using iotc_xamarin_ble.Droid.Services;
using iotc_ble_xamarin;
using iotc_xamarin_ble.Services.BackgroundWorker;
using Newtonsoft.Json;
using Refractored.XamForms.PullToRefresh.Droid;

namespace iotc_xamarin_ble.Droid
{
    [Activity(Label = "IoTC BLE Gateway", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            PullToRefreshLayoutRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            MessagingCenter.Subscribe<RequestMessage<ServiceParameter>>(this, Constants.SERVICE_START, message =>
            {
                var intent = new Intent(this, typeof(IoTCentralService));
                intent.PutExtra(Constants.BLE_DEVICE, message.Data.BLEDeviceId);
                intent.PutExtra(Constants.BLE_MAPPING, JsonConvert.SerializeObject(message.Data.TelemetryMap));
                intent.PutExtra(Constants.DEVICE_ID, message.Data.DeviceCredentials.DeviceId);
                intent.PutExtra(Constants.SCOPE_ID, message.Data.DeviceCredentials.IdScope);
                intent.PutExtra(Constants.SYM_KEY, message.Data.DeviceCredentials.PrimaryKey);
                StartService(intent);
            });

            MessagingCenter.Subscribe<RequestMessage>(this, Constants.SERVICE_STOP, message =>
            {
                var intent = new Intent(this, typeof(IoTCentralService));
                StopService(intent);
            });
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            //App.ParentWindow = new PlatformParameters(this);
            //App.ParentWindow = this;

            // subscribing to service messages

        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }
        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
        //    AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}