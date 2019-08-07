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
            var url = new NSUrl($"prefs:root=WIFI");            UIApplication.SharedApplication.OpenUrl(url);

        }
    }
}