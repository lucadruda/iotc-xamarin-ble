using Foundation;
using iotc_xamarin_ble.iOS.Services;
using iotc_xamarin_ble.Services;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(WiFiManager))]
namespace iotc_xamarin_ble.iOS.Services
{
    public class WiFiManager : IWiFiManager
    {
        public void Connect(string ssid, string passphrase = null)
        {
            throw new NotImplementedException();
        }

        public string GetConnectedAp()
        {
            return "";
        }

        public string GetCurrentIp()
        {
            return "";
        }

        public void ReceiveBroadcast()
        {
            throw new NotImplementedException();
        }

        public void Scan()
        {
            var WiFiURL = new NSUrl("prefs:root=WIFI");if(UIApplication.SharedApplication.CanOpenUrl(WiFiURL)){   //Pre iOS 10    UIApplication.SharedApplication.OpenUrl(WiFiURL);}else{   //iOS 10    UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));}

        }
    }
}