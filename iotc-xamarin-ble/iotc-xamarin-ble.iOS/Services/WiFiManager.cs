using iotc_xamarin_ble.iOS.Services;
using iotc_xamarin_ble.Services;
using System;
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
            throw new NotImplementedException();
        }

        public string GetCurrentIp()
        {
            throw new NotImplementedException();
        }

        public void ReceiveBroadcast()
        {
            throw new NotImplementedException();
        }

        public void Scan()
        {

        }
    }
}